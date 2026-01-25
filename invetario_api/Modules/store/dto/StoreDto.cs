using invetario_api.Modules.store.entity;
using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.store.dto
{
    public class StoreDto
    {
        [Required]
        [MinLength(1)]
        public string name { get; set; }

        [Required]
        public string code { get; set; }

        [Required]
        public string address { get; set; }


        [Required]
        [MaxLength(9)]
        public string phone { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int maxCapacity { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int userId { get; set; }

        public string observations { get; set; }

        [Required]
        public string type { get; set; } = StoredType.PRINCIPAL;
    }
}
