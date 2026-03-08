using System;
using System.ComponentModel.DataAnnotations;
using invetario_api.Utils;

namespace invetario_api.Modules.sale.dto.query;

public class SaleQueryDto : PaginateDto
{
    [Required]
    public int? storeId { get; set; }
}
