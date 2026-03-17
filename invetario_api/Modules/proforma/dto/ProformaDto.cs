using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.proforma.dto
{
  public class ProformaDetailsDto
  {
    [Required]
    public int? productId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int quantity { get; set; }
  }


  public class ProformaDto
  {
    [Required]
    public int? clientId { get; set; }

    [Required]
    public int? storeId { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "At least one product must be included")]
    public List<ProformaDetailsDto> details { get; set; }
  }
}
