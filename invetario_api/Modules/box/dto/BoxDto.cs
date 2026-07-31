using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.box.dto
{
  public class BoxDto
  {
    [Required]
    public string boxName { get; set; }

    [Required]
    public string serie { get; set; }

    [Required]
    public string serieProforma { get; set; }

    [Required]
    public int? storeId { get; set; }
  }
}
