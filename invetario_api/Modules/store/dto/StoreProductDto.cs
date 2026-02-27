using System;
using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.store.entity;

public class StoreProductDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int productId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int actualStock { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int minStock { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int maxStock { get; set; }

    [Required]
    [Range(1, float.MaxValue)]
    public float avgCost { get; set; }

    [Required]
    [Range(1, float.MaxValue)]
    public float lastCost { get; set; }
}
