using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.dayBox.dto
{
  public class DayboxDto
  {
    [Required]
    public int? boxId { get; set; }

    [Required]
    public float? totalefectivo { get; set; }

    [Required]
    public float? totalTarjeta { get; set; }

    public string observations { get; set; }

    [Required]
    public DateTime? date { get; set; }
  }
}
