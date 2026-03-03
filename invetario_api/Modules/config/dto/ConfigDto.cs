using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.config.dto
{
  public class ConfigDto
  {
    [Required]
    public string? enterpriseName { get; set; }

    [Required]
    public string? contactEmail { get; set; }

    [Required]
    public string? ruc { get; set; }

    [Required]
    public string? address { get; set; }

    [Required]
    public string? phone { get; set; }

    [Required]
    [Url]
    public string? logoUrl { get; set; }

    [Required]
    public string? localCurrency { get; set; }
  }
}
