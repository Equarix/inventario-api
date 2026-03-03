using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.sale.dto;
using invetario_api.Modules.sale.entity;
using invetario_api.Modules.sale.response;
using invetario_api.Modules.users.current_user;
using invetario_api.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.sale
{
    public class SaleService : ISaleService
    {
        private Database _db;
        private readonly ICurrentUserService _currentUser;

        public SaleService(Database db, ICurrentUserService currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<List<SaleResponse>> getSales()
        {
            var sales = await _db.sales
                .Include(s => s.client)
                .Include(s => s.user)
                .Include(s => s.saleMethods)
                .ThenInclude(sp => sp.payMethod)
                .Include(s => s.saleDetails)
                .ThenInclude(sd => sd.product)
                .Include(s => s.store)
                .ToListAsync();

            return SaleResponse.FromEntityList(sales);
        }

        public async Task<SaleResponse> createSale(SaleDto data)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var userId = _currentUser.UserId;

                var user = await _db.users
                    .FirstOrDefaultAsync(u => u.userId == userId);

                if (user == null)
                    throw new HttpException(404, "User not found");

                var storeAssignment = await _db.storeUsers
                    .Include(su => su.Store)
                    .FirstOrDefaultAsync(su =>
                        su.StoreId == data.storeId &&
                        su.UserId == userId);

                if (storeAssignment == null)
                    throw new HttpException(403, "User not assigned to the store");

                var client = await _db.clients
                    .FirstOrDefaultAsync(c => c.clientId == data.clientId);

                if (client == null)
                    throw new HttpException(404, "Client not found");

                var saleDetailsDict = data.saleDetails
                    .GroupBy(x => x.productId)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Sum(x => x.quantity)
                    );

                var productIds = saleDetailsDict.Keys.ToList();

                var productStores = await _db.productStores
                    .Where(ps =>
                        ps.storeId == data.storeId &&
                        productIds.Contains(ps.productId))
                    .Include(ps => ps.product)
                    .ToListAsync();

                var notFound = productIds
                    .Except(productStores.Select(ps => ps.productId))
                    .ToList();

                if (notFound.Any())
                    throw new HttpException(404,
                        $"Products with ids {string.Join(", ", notFound)} not found in this store");

                var inactive = productStores
                    .Where(ps => !ps.status)
                    .Select(ps => ps.productId)
                    .ToList();

                if (inactive.Any())
                    throw new HttpException(400,
                        $"Products with ids {string.Join(", ", inactive)} are inactive");

                var noStock = productStores
                    .Where(ps => ps.actualStock < saleDetailsDict[ps.productId])
                    .Select(ps => ps.productId)
                    .ToList();

                if (noStock.Any())
                    throw new HttpException(400,
                        $"Products with ids {string.Join(", ", noStock)} do not have enough stock");

                var total = productStores
                    .Sum(ps =>
                        ps.product.priceSell *
                        saleDetailsDict[ps.productId]);

                var totalPayments = data.saleMethods
                    .Sum(sm => sm.amount);

                if (total != totalPayments)
                    throw new HttpException(400,
                        $"Total of sale ({total}) does not match total of payments ({totalPayments})");

                foreach (var ps in productStores)
                {
                    ps.actualStock -= saleDetailsDict[ps.productId];
                }

                var sale = new Sale
                {
                    clientId = client.clientId,
                    userId = user.userId,
                    storeId = data.storeId,
                    observations = data.observations,
                    total = (float)total,
                    createdAt = DateTime.UtcNow
                };

                _db.sales.Add(sale);

                foreach (var ps in productStores)
                {
                    var quantity = saleDetailsDict[ps.productId];

                    _db.saleDetails.Add(new SaleDetails
                    {
                        sale = sale,
                        productId = ps.productId,
                        productName = ps.product.name,
                        quantity = quantity,
                        priceSell = Decimal.Round((decimal)ps.product.priceSell, 2)
                    });
                }

                var payMethodIds = data.saleMethods
                    .Select(x => x.methodId)
                    .ToList();

                var payMethods = await _db.payMethods
                    .Where(pm => payMethodIds.Contains(pm.paymethodId))
                    .ToListAsync();

                foreach (var methodDto in data.saleMethods)
                {
                    var payMethod = payMethods
                        .FirstOrDefault(pm => pm.paymethodId == methodDto.methodId);

                    if (payMethod == null)
                        throw new HttpException(404,
                            $"Pay method {methodDto.methodId} not found");

                    _db.saleMethods.Add(new SaleMethods
                    {
                        sale = sale,
                        payMethodId = payMethod.paymethodId,
                        amount = methodDto.amount,
                        methodPayment = payMethod.name
                    });
                }

                await _db.SaveChangesAsync();

                await transaction.CommitAsync();

                var createdSale = await _db.sales
                    .Where(s => s.saleId == sale.saleId)
                    .Include(s => s.client)
                    .Include(s => s.user)
                    .Include(s => s.store)
                    .Include(s => s.saleDetails)
                        .ThenInclude(sd => sd.product)
                    .Include(s => s.saleMethods)
                        .ThenInclude(sm => sm.payMethod)
                    .FirstOrDefaultAsync();

                return SaleResponse.FromEntity(createdSale!);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<SaleResponse?> deleteSale(int saleId)
        {
            throw new NotImplementedException();
        }

        public async Task<SaleResponse?> getSaleById(int saleId)
        {
            throw new NotImplementedException();
        }
    }
}
