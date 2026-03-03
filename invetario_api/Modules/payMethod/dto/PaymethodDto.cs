using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.payMethod.dto
{
  public class PaymethodDto
  {
    [Required]
    [MinLength(3)]
    public string name { get; set; }

    public bool turned { get; set; } = false;
  }
}
