using System;
using invetario_api.Modules.products.response;

namespace invetario_api.Modules.sale.response;

public class SaleDetailsResponse
{
    public int saleDetailId { get; set; }
    public ProductSingleResponse product { get; set; }
    public string productName { get; set; }
    public int quantity { get; set; }
    public decimal priceSell { get; set; }

    public static SaleDetailsResponse FromEntity(entity.SaleDetails saleDetail)
    {
        return new SaleDetailsResponse
        {
            saleDetailId = saleDetail.saleDetailId,
            product = ProductSingleResponse.fromEntity(saleDetail.product),
            productName = saleDetail.productName,
            quantity = saleDetail.quantity,
            priceSell = saleDetail.priceSell
        };
    }

    public static List<SaleDetailsResponse> FromEntityList(List<entity.SaleDetails> saleDetails)
    {
        return saleDetails.Select(FromEntity).ToList();
    }
}
