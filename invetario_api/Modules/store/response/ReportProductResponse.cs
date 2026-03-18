using System;
using invetario_api.Modules.products.response;
using invetario_api.Modules.users.response;

namespace invetario_api.Modules.store.response;

public class ReportProductResponse
{
    public int reportProductId { get; set; }

    public ProductSingleResponse product { get; set; }

    public int quantity { get; set; }

    public DateTime date { get; set; } = DateTime.Now;

    public StoreSingleResponse store { get; set; }

    public UserSingleResponse user { get; set; }


    public static ReportProductResponse fromEntity(entity.ReportProduct reportProduct)
    {
        return new ReportProductResponse
        {
            reportProductId = reportProduct.reportProductId,
            product = ProductSingleResponse.fromEntity(reportProduct.product),
            quantity = reportProduct.quantity,
            date = reportProduct.date,
            store = StoreSingleResponse.fromEntity(reportProduct.store),
            user = UserSingleResponse.fromEntity(reportProduct.user)
        };
    }

    public static List<ReportProductResponse> fromEntityList(List<entity.ReportProduct> reportProducts)
    {
        return reportProducts.Select(fromEntity).ToList();
    }
}
