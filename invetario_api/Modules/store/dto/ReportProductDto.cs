using System;
using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.store.dto;

public class ReportProductDto
{
    [Required]
    public int? productId { get; set; }

    [Required]
    public int? quantity { get; set; }

    public DateTime date { get; set; } = DateTime.Now;
    [Required]
    public int? storeId { get; set; }
}
