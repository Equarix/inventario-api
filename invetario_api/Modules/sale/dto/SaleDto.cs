using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.sale.dto
{
  public class SaleDto
  {
    [Required]
    [Range(1, int.MaxValue)]
    public int clientId { get; set; }

    public string observations { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue)]
    public int storeId { get; set; }

    [Required]
    [MinLength(1)]
    public List<SaleMethodsDto> saleMethods { get; set; }

    [Required]
    [MinLength(1)]
    public List<SaleDetailsDto> saleDetails { get; set; }
  }
}
