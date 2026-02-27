using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.payMethod.dto
{
    public class UpdatePaymethodDto : PaymethodDto
    {
        [Required]
        public bool? status { get; set; }
    }
}
