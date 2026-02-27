using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.box.dto
{
  public class BoxDto
  {
    [Required]
    [Range(typeof(decimal), "1", "79228162514264337593543950335",
        ErrorMessage = "Amount must be a positive number.")]
    public decimal amountOpening { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive integer.")]
    public int userActualId { get; set; }
  }
}
