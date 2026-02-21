using System.ComponentModel.DataAnnotations;
using invetario_api.Modules.provider.entity;

namespace invetario_api.Modules.provider.dto
{
  public class ProviderDto
  {
    [Required]
    [MinLength(3)]
    public string code { get; set; }
    [Required]
    [MinLength(1)]
    public string companyName { get; set; }
    [Required]
    [MinLength(1)]
    public string publicName { get; set; }
    [Required]
    [MinLength(3)]
    public string typeDocument { get; set; }
    [Required]
    [MinLength(1)]
    public string documentNumber { get; set; }
    public string address { get; set; }
    [Required]
    [MinLength(1)]
    public string phone { get; set; }

    [EmailAddress]
    public string email { get; set; }
    [Required]
    [MinLength(1)]
    public string mainContact { get; set; }
    [Required]
    [MinLength(1)]
    public string contactPhone { get; set; }
    [Required]
    [EnumDataType(typeof(PayCondition))]
    public PayCondition? payCondition { get; set; }
    [Required]
    [MinLength(1)]
    public string typeMoney { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int daysDelivery { get; set; }
  }
}
