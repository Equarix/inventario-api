using System;
using invetario_api.Modules.products.entity;

namespace invetario_api.Modules.products.response;

public class ProductSingleResponse
{
    public int productId { get; set; }
    public string codeInternal { get; set; }
    public string code { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public float priceBuy { get; set; }
    public float priceSell { get; set; }
    public int minStock { get; set; }
    public bool status { get; set; }
    public static ProductSingleResponse fromEntity(Product product)
    {
        return new ProductSingleResponse
        {
            productId = product.productId,
            codeInternal = product.codeInternal,
            code = product.code,
            name = product.name,
            description = product.description,
            priceBuy = product.priceBuy,
            priceSell = product.priceSell,
            minStock = product.minStock,
            status = product.status
        };
    }

    public static List<ProductSingleResponse> fromEntityList(List<Product> products)
    {
        return products.Select(p => fromEntity(p)).ToList();
    }
}
