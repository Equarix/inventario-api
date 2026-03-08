using System.ComponentModel.DataAnnotations;
using invetario_api.Modules.users.entity;

namespace invetario_api.Modules.users.dto
{
    public class UserDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [MinLength(3)]
        public string password { get; set; }

        [Required]
        [MinLength(2)]
        public string firstName { get; set; }

        [Required]
        [MinLength(2)]
        public string lastName { get; set; }

        [EnumDataType(typeof(Role))]
        public Role? role { get; set; }
    }
}
