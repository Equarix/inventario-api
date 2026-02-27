using System;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.products.response;

namespace invetario_api.Modules.store.response;

public class StoreProductResponse
{
    public int productStoreId { get; set; }

    public ProductResponse product { get; set; }

    public int actualStock { get; set; }

    public int minStock { get; set; }

    public int maxStock { get; set; }

    public float avgCost { get; set; }

    public float lastCost { get; set; }

    public bool status { get; set; }

    public DateTime createdAt { get; set; }


    public static StoreProductResponse fromEntity(ProductStore storeProduct)
    {

        return new StoreProductResponse
        {
            productStoreId = storeProduct.productStoreId,
            product = ProductResponse.fromEntity(storeProduct.product),
            actualStock = storeProduct.actualStock,
            minStock = storeProduct.minStock,
            maxStock = storeProduct.maxStock,
            avgCost = storeProduct.avgCost,
            lastCost = storeProduct.lastCost,
            status = storeProduct.status,
            createdAt = storeProduct.createdAt,
        };
    }

    public static List<StoreProductResponse> fromEntityList(List<ProductStore> storeProducts)
    {
        return storeProducts.Select(storeProduct => fromEntity(storeProduct)).ToList();
    }
}
