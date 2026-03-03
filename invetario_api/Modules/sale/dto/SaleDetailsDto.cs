using System;
using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.sale.dto;

public class SaleDetailsDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int productId { get; set; }

    [Required]
    [Range(0, float.MaxValue)]
    public int quantity { get; set; }

}
