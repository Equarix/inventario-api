using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.entryorder.dto;
using invetario_api.Modules.entryorder.entity;
using invetario_api.Modules.entryorder.enums;
using invetario_api.Modules.entryorder.response;
using invetario_api.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.entryorder
{
    public class EntryorderService : IEntryorderService
    {
        private Database _db;

        public EntryorderService(Database db)
        {
            _db = db;
        }

        public async Task<EntryOrderResponse?> cancelEntryorder(int entryorderId)
        {
            var entryorder = await _db.entryorders
                .Where(e => e.entryOrderId == entryorderId)
                .Include(e => e.provider)
                .Include(e => e.store)
                    .ThenInclude(s => s.user)
                .Include(e => e.entryOrderDetails)
                .ThenInclude(d => d.product).ThenInclude(p => p.category)
                .Include(e => e.entryOrderDetails).ThenInclude(d => d.product).ThenInclude(p => p.unit)
                .FirstOrDefaultAsync();

            if (entryorder == null)
            {
                throw new HttpException(404, "Entryorder not found");
            }

            entryorder.entryOrderStatus = EntryOrderStatus.CANCELLED;
            await _db.SaveChangesAsync();
            return EntryOrderResponse.fromEntity(entryorder);
        }

        public async Task<EntryOrderResponse?> completeEntryorder(int entryorderId)
        {
            var entryorder = await _db.entryorders
                .Where(e => e.entryOrderId == entryorderId)
                .Include(e => e.provider)
                .Include(e => e.store)
                    .ThenInclude(s => s.user)
                .Include(e => e.entryOrderDetails)
                .ThenInclude(d => d.product).ThenInclude(p => p.category)
                .Include(e => e.entryOrderDetails).ThenInclude(d => d.product).ThenInclude(p => p.unit)
                .FirstOrDefaultAsync();

            if (entryorder == null)
            {
                throw new HttpException(404, "Entryorder not found");
            }

            entryorder.entryOrderStatus = EntryOrderStatus.COMPLETED;

            foreach (var detail in entryorder.entryOrderDetails)
            {
                var productStore = await _db.productStores.FirstOrDefaultAsync(ps => ps.productId == detail.productId && ps.storeId == entryorder.storeId);
                if (productStore != null)
                {
                    productStore.actualStock += detail.quantity;
                }
            }
            await _db.SaveChangesAsync();
            return EntryOrderResponse.fromEntity(entryorder);
        }

        public async Task<EntryOrderResponse> createEntryorder(EntryorderDto data)
        {
            var findProvider = await _db.providers.FirstOrDefaultAsync(p => p.providerId == data.providerId);

            if (findProvider == null)
            {
                throw new HttpException(404, "Provider not found");
            }

            var findStore = await _db.stores.Where(s => s.storeId == data.storeId).Include(s => s.user).FirstOrDefaultAsync();

            if (findStore == null)
            {
                throw new HttpException(404, "Store not found");
            }

            var newEntryorder = new Entryorder
            {
                providerId = data.providerId,
                provider = findProvider,
                storeId = data.storeId,
                store = findStore,
                entryDate = data.entryDate,
                entryOrderType = data.entryOrderType.Value,
                typeMoney = data.typeMoney,
                payCondition = data.payCondition.Value,
                tax = data.tax,
                observation = data.observation,
            };

            await _db.entryorders.AddAsync(newEntryorder);

            foreach (var detail in data.entryOrderDetails)
            {
                var product = await _db.products.Include(p => p.category).Include(p => p.unit).FirstOrDefaultAsync(p => p.productId == detail.productId);

                if (product == null)
                {
                    throw new HttpException(404, $"Product with ID {detail.productId} not found");
                }

                var findProductInStore = await _db.productStores.FirstOrDefaultAsync(ps => ps.productId == detail.productId && ps.storeId == data.storeId);

                if (findProductInStore == null)
                {
                    throw new HttpException(400, $"Product with ID {detail.productId} is not available in Store with ID {data.storeId}");
                }

                var newDetail = new EntryOrderDetail
                {
                    entryorder = newEntryorder,
                    productId = detail.productId,
                    quantity = detail.quantity,
                    unitPrice = detail.unitPrice,
                    product = product,
                };
                await _db.entryOrderDetails.AddAsync(newDetail);
                newEntryorder.entryOrderDetails.Add(newDetail);
            }
            await _db.SaveChangesAsync();
            return EntryOrderResponse.fromEntity(newEntryorder);
        }

        public async Task<EntryOrderResponse?> getEntryorderById(int entryorderId)
        {
            var entryorder = await _db.entryorders
                .Where(e => e.entryOrderId == entryorderId)
                .Include(e => e.provider)
                .Include(e => e.store)
                    .ThenInclude(s => s.user)
                .Include(e => e.entryOrderDetails)
                .ThenInclude(d => d.product).ThenInclude(p => p.category)
                .Include(e => e.entryOrderDetails).ThenInclude(d => d.product).ThenInclude(p => p.unit)
                .FirstOrDefaultAsync();

            if (entryorder == null)
            {
                throw new HttpException(404, "Entryorder not found");
            }

            return EntryOrderResponse.fromEntity(entryorder);
        }

        public async Task<List<EntryOrderResponse>> getEntryorders()
        {
            var entryorder = await _db.entryorders
                .Include(e => e.provider)
                .Include(e => e.store)
                    .ThenInclude(s => s.user)
                .Include(e => e.entryOrderDetails)
                .ThenInclude(d => d.product).ThenInclude(p => p.category)
                .Include(e => e.entryOrderDetails).ThenInclude(d => d.product).ThenInclude(p => p.unit)
                .ToListAsync();
            return EntryOrderResponse.fromEntityList(entryorder);
        }
    }
}
