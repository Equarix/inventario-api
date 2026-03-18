using invetario_api.Modules.store.dto;
using invetario_api.Modules.store.entity;
using invetario_api.Modules.store.response;
using invetario_api.utils;
using invetario_api.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.store
{
    public interface IStoreService
    {
        Task<List<StoreResponse>> getStores();

        Task<StoreResponse?> getStoreById(int storeId);

        Task<StoreResponse?> createStore(StoreDto data);

        Task<StoreResponse?> updateStore(int storeId, UpdateStoreDto data);

        Task<StoreResponse?> deleteStore(int storeId);

        Task<StoreProductResponse?> addProductToStore(int storeId, StoreProductDto data);

        Task<StoreProductResponse?> updateStoreProduct(int storeId, int productStoreId, UpdateStoreProductDto data);

        Task<StoreProductResponse> removeProductFromStore(int storeId, int productStoreId);

        Task<PageResult<List<StoreProductResponse>>> getProductsByStore(PaginateDto paginate, int storeId);
        Task<StoreProductResponse?> getStoreProductById(int storeId, int productStoreId);

        Task<List<StoreProductResponse>> searchByStoreIdAndName(string name, int storeId);


        Task<PageResult<List<ReportProductResponse>>> getReportProducts(PaginateDto paginate);

        Task<ReportProductResponse> createReportProduct(ReportProductDto data);
    }
}
