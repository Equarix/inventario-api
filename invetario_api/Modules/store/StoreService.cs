using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.proforma.dto;
using invetario_api.Modules.store.dto;
using invetario_api.Modules.store.entity;
using invetario_api.Modules.store.response;
using invetario_api.Modules.users.current_user;
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

        private ICurrentUserService _currentUserService;

        public StoreService(Database db, ICurrentUserService currentUserService)
        {
            _db = db;
            _currentUserService = currentUserService;
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

        public async Task<StoreProductResponse?> getStoreProductById(int storeId, int productStoreId)
        {
            var productStore = await _db.productStores
                .Include(ps => ps.product)
                    .ThenInclude(p => p.category)
                    .Include(ps => ps.product)
                    .ThenInclude(p => p.unit)
                    .Include(ps => ps.product)
                    .ThenInclude(p => p.image)
                .Where(ps => ps.storeId == storeId && ps.productStoreId == productStoreId)
                .FirstOrDefaultAsync();

            if (productStore == null)
            {
                throw new HttpException(404, "Product in store not found");
            }

            return StoreProductResponse.fromEntity(productStore);
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

            productStore.actualStock = data.actualStock;
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

        public async Task<List<StoreProductResponse>> getProductsByStoreV1(int storeId)
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

        public async Task<PageResult<List<StoreProductResponse>>> getProductsByStore(PaginateDto paginate, int storeId)
        {
            var query = _db.productStores.AsQueryable();

            var count = await query.Where(ps => ps.storeId == storeId).CountAsync();


            var storeProducts = await _db.productStores
                .Include(ps => ps.product)
                    .ThenInclude(p => p.category)
                    .Include(ps => ps.product)
                    .ThenInclude(p => p.unit)
                    .Include(ps => ps.product)
                    .ThenInclude(p => p.image)
                .Where(ps => ps.storeId == storeId)
                .Skip((paginate.page - 1) * paginate.limit)
                .Take(paginate.limit)
                .ToListAsync();

            var storeResponse = StoreProductResponse.fromEntityList(storeProducts);

            return new PageResult<List<StoreProductResponse>>
            {
                items = storeResponse,
                totalItems = count,
                limit = paginate.limit,
                page = paginate.page,
            };
        }



        public async Task<List<StoreProductResponse>> searchByStoreIdAndName(string name, int storeId)
        {
            var storeProducts = await _db.productStores
                .Include(ps => ps.product)
                    .ThenInclude(p => p.category)
                    .Include(ps => ps.product)
                    .ThenInclude(p => p.unit)
                    .Include(ps => ps.product)
                    .ThenInclude(p => p.image)
                .Where(ps => ps.storeId == storeId && ps.product.name.Contains(name))
                .ToListAsync();

            return StoreProductResponse.fromEntityList(storeProducts);
        }

        public async Task<PageResult<List<ReportProductResponse>>> getReportProducts(PaginateDto paginate)
        {
            var query = _db.reportProducts.AsQueryable();

            var count = await query.CountAsync();

            var reportProducts = await _db.reportProducts
                .Include(rp => rp.product)
                    .ThenInclude(p => p.category)
                    .Include(rp => rp.product)
                    .ThenInclude(p => p.unit)
                    .Include(rp => rp.product)
                    .ThenInclude(p => p.image)
                .Include(rp => rp.store)
                .Include(rp => rp.user)
                .Skip((paginate.page - 1) * paginate.limit)
                .Take(paginate.limit)
                .ToListAsync();

            return new PageResult<List<ReportProductResponse>>
            {
                items = ReportProductResponse.fromEntityList(reportProducts),
                totalItems = count,
                limit = paginate.limit,
                page = paginate.page,
            };
        }

        public async Task<ReportProductResponse> createReportProduct(ReportProductDto data)
        {
            var user = await _currentUserService.GetCurrentUser();

            var findStore = await _db.stores
                .Where(s => s.storeId == data.storeId && s.status == true)
                .FirstOrDefaultAsync();

            if (findStore == null)
            {
                throw new HttpException(404, "Store not found");
            }

            var findProduct = await _db.products
                .Where(p => p.productId == data.productId && p.status == true)
                .FirstOrDefaultAsync();

            if (findProduct == null)
            {
                throw new HttpException(404, "Product not found");
            }

            var newReportProduct = new ReportProduct
            {
                productId = data.productId!.Value,
                quantity = data.quantity!.Value,
                date = data.date,
                storeId = data.storeId!.Value,
                userId = user!.userId,
                product = findProduct,
                store = findStore,
                user = user
            };

            await _db.reportProducts.AddAsync(newReportProduct);
            await _db.SaveChangesAsync();

            return ReportProductResponse.fromEntity(newReportProduct);
        }
    }
}
