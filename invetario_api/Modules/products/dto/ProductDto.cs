using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.products.dto
{
    public class ProductDto
    {
        [Required]
        [MinLength(1)]
        public string codeInternal { get; set; }

        [Required]
        [MinLength(1)]
        public string code { get; set; }

        [Required]
        [MinLength(1)]
        public string name { get; set; }

        public string description { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int categoryId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int unitId { get; set; }

        [Required]
        [Range(1.00, float.MaxValue)]
        public float priceBuy { get; set; }

        [Required]
        [Range(1.00, float.MaxValue)]
        public float priceSell { get; set; }


        [Required]
        [Range(1, int.MaxValue)]
        public int minStock { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int imageId { get; set; }
    }
}
