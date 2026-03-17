using System;
using System.ComponentModel.DataAnnotations;
using invetario_api.Utils;

namespace invetario_api.Modules.proforma.dto;

public class QueryDto : PaginateDto
{
    [Required]
    public int? storeId { get; set; }
}
