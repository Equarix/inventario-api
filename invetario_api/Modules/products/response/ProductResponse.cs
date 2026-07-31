
using invetario_api.Modules.categories.response;
using invetario_api.Modules.images.response;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.unit.response;
using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.products.response
{
    public class ProductResponse
    {
        public int productId { get; set; }
        public string codeInternal { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public CategorySingleResponse category { get; set; }

        public UnitSingleResponse unit { get; set; }

        public float priceBuy { get; set; }
        public float priceSell { get; set; }
        public int minStock { get; set; }
        public bool status { get; set; }
        public ImageResponse image { get; set; }

        public ICollection<ProductPriceResponse> productPrices { get; set; } = new List<ProductPriceResponse>();

        public static ProductResponse fromEntity(entity.Product product)
        {
            return new ProductResponse
            {
                productId = product.productId,
                codeInternal = product.codeInternal,
                code = product.code,
                name = product.name,
                description = product.description,
                category = CategorySingleResponse.fromEntity(product.category),
                unit = UnitSingleResponse.fromEntity(product.unit),
                priceBuy = product.priceBuy,
                priceSell = product.priceSell,
                minStock = product.minStock,
                status = product.status,
                image = ImageResponse.FromEntity(product.image),
                productPrices = ProductPriceResponse.fromEntityList(product.productPrices.ToList())
            };
        }

        public static List<ProductResponse> fromEntityList(List<Product> products)
        {
            return products.Select(p => fromEntity(p)).ToList();
        }
    }


    public class ProductPriceResponse
    {
        public int productPriceId { get; set; }
        public float price { get; set; }

        public DateTime createdAt { get; set; }

        public bool status { get; set; }

        public static ProductPriceResponse fromEntity(ProductPrices productPrice)
        {
            return new ProductPriceResponse
            {
                productPriceId = productPrice.productPriceId,
                price = productPrice.price,
                status = productPrice.status,
                createdAt = productPrice.createdAt
            };
        }

        public static List<ProductPriceResponse> fromEntityList(List<ProductPrices> productPrices)
        {
            return productPrices.Select(p => fromEntity(p)).ToList();
        }
    }
}
