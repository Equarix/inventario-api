using System;
using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.products.dto;

public class ReportProductQueryDto
{
    [Required]
    public int? storeId { get; set; }

    [Required]
    public DateTime? startDate { get; set; }

    [Required]
    public DateTime? endDate { get; set; }
}
