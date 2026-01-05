using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.provider.dto
{
    public class UpdateProviderDto : ProviderDto
    {
        [Required]
        public bool? status { get; set; }
    }
}
