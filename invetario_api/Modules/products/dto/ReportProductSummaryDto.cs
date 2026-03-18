using System;
using invetario_api.Modules.products.response;
using invetario_api.Modules.store.response;
using invetario_api.Modules.users.response;

namespace invetario_api.Modules.products.dto;

public class ReportProductSummaryDto
{
    public int productId { get; set; }
    public int storeId { get; set; }
    public int totalQuantity { get; set; }
    public ProductSingleResponse? product { get; set; }
    public StoreSingleResponse? store { get; set; }
}
