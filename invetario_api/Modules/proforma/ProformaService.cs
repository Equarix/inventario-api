using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.proforma.dto;
using invetario_api.Modules.proforma.entity;
using invetario_api.Modules.proforma.response;
using invetario_api.Modules.users.current_user;
using invetario_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace invetario_api.Modules.proforma
{
    public class ProformaService : IProformaService
    {
        private Database _db;
        private readonly ICurrentUserService _currentUser;

        public ProformaService(Database db, ICurrentUserService currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<PageResult<List<ProformaResponse>>> getProformas(QueryDto paginate)
        {
            var allProformas = paginate.storeId!.Value == 0;

            var query = _db.proformas.AsQueryable();

            var totalItems = allProformas ? await query.CountAsync() : await query.Where(p => p.storeId == paginate.storeId!.Value).CountAsync();

            var queryItems = query
                .Include(x => x.client)
                .Include(x => x.store)
                .Include(x => x.user)
                .Include(x => x.details)
                    .ThenInclude(x => x.product);

            var filteredQuery = allProformas ? queryItems : queryItems.Where(p => p.storeId == paginate.storeId!.Value);

            var items = await filteredQuery.OrderByDescending(p => p.createdAt)
            .Skip((paginate.page - 1) * paginate.limit)
            .Take(paginate.limit)
            .ToListAsync();

            return new PageResult<List<ProformaResponse>>()
            {
                items = ProformaResponse.fromProformaList(items),
                totalItems = totalItems,
                page = paginate.page,
                limit = paginate.limit
            };
        }

        public async Task<ProformaResponse> createProforma(ProformaDto data)
        {
            var currentUser = await _currentUser.GetCurrentUser();

            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var findClient = await _db.clients.Where(x => x.clientId == data.clientId).FirstOrDefaultAsync();

                if (findClient == null)
                    throw new HttpException(404, "Client not found");

                var findStore = await _db.stores.Where(x => x.storeId == data.storeId).FirstOrDefaultAsync();

                if (findStore == null)
                    throw new HttpException(404, "Store not found");

                var proforma = new Proforma()
                {
                    client = findClient,
                    store = findStore,
                    clientId = data.clientId!.Value,
                    storeId = data.storeId!.Value,
                    user = currentUser!,
                    userId = currentUser!.userId,
                };

                await _db.proformas.AddAsync(proforma);

                foreach (var d in data.details)
                {
                    var findProduct = await _db.products.Where(x => x.productId == d.productId).FirstOrDefaultAsync();

                    if (findProduct == null)
                        throw new HttpException(404, $"Product with id {d.productId} not found");

                    var proformaDetails = new ProformaDetails()
                    {
                        proforma = proforma,
                        proformaId = proforma.proformaId,
                        product = findProduct,
                        productId = d.productId!.Value,
                        quantity = d.quantity,
                        productName = findProduct.name,
                        price = (decimal)findProduct.priceSell,
                    };

                    await _db.proformaDetails.AddAsync(proformaDetails);
                    proforma.details.Add(proformaDetails);
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return ProformaResponse.fromProforma(proforma);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task<ProformaResponse?> getProformaById(int proformaId)
        {
            var proforma = await _db.proformas
                .Include(x => x.client)
                .Include(x => x.store)
                .Include(x => x.user)
                .Include(x => x.details)
                    .ThenInclude(x => x.product)
                .Where(x => x.proformaId == proformaId)
                .FirstOrDefaultAsync();

            if (proforma == null)
                throw new HttpException(404, "Proforma not found");


            return ProformaResponse.fromProforma(proforma);
        }

    }
}
