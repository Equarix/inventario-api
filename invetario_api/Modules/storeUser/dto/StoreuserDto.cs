using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.storeUser.dto
{
  public class StoreuserDto
  {
    [Required]
    public int? storeId { get; set; }

    [Required]
    public int? userId { get; set; }
  }
}
