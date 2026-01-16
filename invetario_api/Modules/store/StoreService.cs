using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.store.dto;
using invetario_api.Modules.store.entity;
using invetario_api.Modules.store.response;
using invetario_api.utils;
using invetario_api.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.store
{
    public class StoreService : IStoreService
    {
        private Database _db;

        public StoreService(Database db)
        {
            _db = db;
        }

        public async Task<List<StoreResponse>> getStores()
        {
            var stores = await _db.stores.Include(s => s.user).ToListAsync();

            return StoreResponse.fromEntityList(stores);
        }

        public async Task<StoreResponse?> createStore(StoreDto data)
        {
            var findUser = await _db.users
                .Where(user => user.userId == data.userId && user.status == true)
                .FirstOrDefaultAsync();

            if (findUser == null)
            {
                throw new HttpException(404, "User not found");
            }

            var newStore = new Store
            {
                name = data.name,
                code = data.code,
                address = data.address,
                phone = data.phone,
                maxCapacity = data.maxCapacity,
                status = true,
                type = data.type,
                userId = data.userId,
                observations = data.observations
            };

            await _db.stores.AddAsync(newStore);
            await _db.SaveChangesAsync();


            var storeResponse = StoreResponse.fromEntity(newStore);

            return storeResponse;
        }

        public async Task<StoreResponse?> deleteStore(int storeId)
        {
            var store = await _db.stores.Where(s => s.storeId == storeId).Include(s => s.user).FirstOrDefaultAsync();
            if (store == null)
            {
                throw new HttpException(404, "Store not found");
            }

            store.status = false;
            await _db.SaveChangesAsync();
            return StoreResponse.fromEntity(store);
        }

        public async Task<StoreResponse?> getStoreById(int storeId)
        {
            var store = await _db.stores.Include(s => s.user).Where(s => s.storeId == storeId).FirstOrDefaultAsync();
            if (store == null)
            {
                throw new HttpException(404, "Store not found");
            }

            return StoreResponse.fromEntity(store);
        }

        public async Task<StoreResponse?> updateStore(int storeId, UpdateStoreDto data)
        {
            var store = await _db.stores.FindAsync(storeId);
            if (store == null)
            {
                throw new HttpException(404, "Store not found");
            }

            var findUser = await _db.users
                .Where(user => user.userId == data.userId && user.status == true)
                .FirstOrDefaultAsync();

            if (findUser == null)
            {
                throw new HttpException(404, "User not found");
            }


            store.name = data.name;
            store.code = data.code;
            store.address = data.address;
            store.phone = data.phone;
            store.maxCapacity = data.maxCapacity;
            store.status = data.status!.Value;
            store.type = data.type;
            store.userId = data.userId;
            store.observations = data.observations;
            await _db.SaveChangesAsync();


            return StoreResponse.fromEntity(store);
        }

        public async Task<StoreProductResponse?> addProductToStore(int storeId, StoreProductDto data)
        {
            var findStore = await _db.stores
                .Where(s => s.storeId == storeId && s.status == true)
                .FirstOrDefaultAsync();

            if (findStore == null)
            {
                throw new HttpException(404, "Store not found");
            }

            var findProduct = await _db.products
                .Where(p => p.productId == data.productId && p.status == true)
                .Include(p => p.category)
                .Include(p => p.unit)
                .Include(p => p.image)
                .FirstOrDefaultAsync();

            if (findProduct == null)
            {
                throw new HttpException(404, "Product not found");
            }

            var findProductStore = await _db.productStores
                .Where(ps => ps.storeId == storeId && ps.productId == data.productId && ps.status == true).FirstOrDefaultAsync();

            if (findProductStore != null)
            {
                throw new HttpException(400, "Product already exists in store");
            }


            var newProductStore = new ProductStore
            {
                productId = data.productId,
                storeId = storeId,
                actualStock = data.actualStock,
                reservedStock = data.reservedStock,
                availableStock = data.availableStock,
                minStock = data.minStock,
                maxStock = data.maxStock,
                avgCost = data.avgCost,
                lastCost = data.lastCost,
                status = true
            };

            await _db.productStores.AddAsync(newProductStore);
            await _db.SaveChangesAsync();

            newProductStore.product = findProduct;

            var storeProductResponse = StoreProductResponse.fromEntity(newProductStore);

            return storeProductResponse;
        }

        public async Task<StoreProductResponse?> updateStoreProduct(int storeId, int productStoreId, UpdateStoreProductDto data)
        {
            var findStore = await _db.stores
                .Where(s => s.storeId == storeId && s.status == true)
                .FirstOrDefaultAsync();

            if (findStore == null)
            {
                throw new HttpException(404, "Store not found");
            }

            var findProduct = await _db.products
                .Where(p => p.productId == data.productId)
                .Include(p => p.category)
                .Include(p => p.unit)
                .Include(p => p.image)
                .FirstOrDefaultAsync();

            if (findProduct == null)
            {
                throw new HttpException(404, "Product not found");
            }

            var findProductStore = await _db.productStores
                .Where(ps => ps.storeId == storeId && ps.productId == data.productId && ps.status == true).FirstOrDefaultAsync();

            if (findProductStore != null)
            {
                throw new HttpException(400, "Product already exists in store");
            }

            var productStore = await _db.productStores
                .Where(ps => ps.productStoreId == productStoreId && ps.storeId == storeId)
                .Include(ps => ps.product)
                    .ThenInclude(p => p.category)
                    .Include(ps => ps.product)
                    .ThenInclude(p => p.unit)
                    .Include(ps => ps.product)
                    .ThenInclude(p => p.image)
                .FirstOrDefaultAsync();

            if (productStore == null)
            {
                throw new HttpException(404, "Product in store not found");
            }

            productStore.productId = data.productId;
            productStore.actualStock = data.actualStock;
            productStore.reservedStock = data.reservedStock;
            productStore.availableStock = data.availableStock;
            productStore.minStock = data.minStock;
            productStore.maxStock = data.maxStock;
            productStore.avgCost = data.avgCost;
            productStore.lastCost = data.lastCost;
            productStore.status = data.status!.Value;

            await _db.SaveChangesAsync();

            productStore.product = findProduct;

            return StoreProductResponse.fromEntity(productStore);
        }

        public async Task<StoreProductResponse> removeProductFromStore(int storeId, int productStoreId)
        {
            var productStore = await _db.productStores
                .Where(ps => ps.productStoreId == productStoreId && ps.storeId == storeId && ps.status == true)
                .Include(ps => ps.product)
                    .ThenInclude(p => p.category)
                    .Include(ps => ps.product)
                    .ThenInclude(p => p.unit)
                    .Include(ps => ps.product)
                    .ThenInclude(p => p.image)
                .FirstOrDefaultAsync();

            if (productStore == null)
            {
                throw new HttpException(404, "Product in store not found");
            }

            productStore.status = false;

            await _db.SaveChangesAsync();
            return StoreProductResponse.fromEntity(productStore);
        }

        public async Task<List<StoreProductResponse>> getProductsByStore(int storeId)
        {
            Console.WriteLine("Fetching products for store ID: " + storeId);
            var storeProducts = await _db.productStores
                .Include(ps => ps.product)
                    .ThenInclude(p => p.category)
                    .Include(ps => ps.product)
                    .ThenInclude(p => p.unit)
                    .Include(ps => ps.product)
                    .ThenInclude(p => p.image)
                .Where(ps => ps.storeId == storeId)
                .ToListAsync();

            return StoreProductResponse.fromEntityList(storeProducts);
        }
    }
}
