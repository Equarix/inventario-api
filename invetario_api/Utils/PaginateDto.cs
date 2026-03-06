using System;
using System.ComponentModel.DataAnnotations;

namespace invetario_api.Utils;

public class PaginateDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0.")]
    public int page { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "Limit size must be greater than 0.")]
    public int limit { get; set; }
}
