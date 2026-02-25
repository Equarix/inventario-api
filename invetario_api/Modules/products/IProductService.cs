using invetario_api.Modules.products.dto;
using invetario_api.Modules.products.response;

namespace invetario_api.Modules.products
{
    public interface IProductService
    {
        Task<List<ProductResponse>> getProducts();

        Task<ProductResponse?> getProductById(int productId);

        Task<List<ProductResponse>> getProudctsByStoreId(int storeId);

        Task<ProductResponse?> createProduct(ProductDto product);

        Task<ProductResponse?> updateProduct(int productId, UpdateProductDto product);

        Task<ProductResponse?> deleteProduct(int productId);
    }
}
