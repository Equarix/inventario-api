using System;
using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.sale.dto;

public class SaleMethodsDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int methodId { get; set; }

    [Required]
    [Range(0, float.MaxValue)]
    public float amount { get; set; }
}
