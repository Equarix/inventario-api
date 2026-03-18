using invetario_api.Modules.products.dto;
using invetario_api.Modules.products.response;
using invetario_api.Modules.store.response;
using invetario_api.Utils;

namespace invetario_api.Modules.products
{
    public interface IProductService
    {
        Task<PageResult<List<ProductResponse>>> getProducts(PaginateDto paginate);

        Task<object> getKpi();

        Task<ProductResponse?> getProductById(int productId);

        Task<List<ProductResponse>> getProudctsByStoreId(int storeId);

        Task<ProductResponse?> createProduct(ProductDto product);

        Task<ProductResponse?> updateProduct(int productId, UpdateProductDto product);

        Task<List<ReportProductSummaryDto>> getReportProducts(ReportProductQueryDto query);

        Task<ProductResponse?> deleteProduct(int productId);
    }
}
