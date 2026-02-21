using System.ComponentModel.DataAnnotations;
using invetario_api.Modules.client.entity;

namespace invetario_api.Modules.client.dto
{
  public class ClientDto
  {
    public ClientType clientType { get; set; } = ClientType.NATURAL;

    [Required]
    [MinLength(3)]
    public string name { get; set; }

    [Required]
    [MinLength(3)]
    public string typeDocument { get; set; }

    [Required]
    [MinLength(3)]
    public string documentNumber { get; set; }

    [Required]
    [MinLength(9)]
    public string phone { get; set; }
    public string email { get; set; } = string.Empty;

  }
}
