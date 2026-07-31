using invetario_api.Modules.categories.entity;
using invetario_api.Modules.entryorder.entity;
using invetario_api.Modules.images.entity;
using invetario_api.Modules.unit.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.products.entity
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; }
        [Required]
        public string codeInternal { get; set; }
        [Required]
        public string code { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public int categoryId { get; set; }

        [ForeignKey(nameof(categoryId))]
        public Category category { get; set; }


        [Required]
        public int unitId { get; set; }

        [ForeignKey(nameof(unitId))]
        public Unit unit { get; set; }

        [Required]
        public float priceBuy { get; set; }

        [Required]
        public float priceSell { get; set; }

        [Required]
        public int minStock { get; set; }

        public bool status { get; set; } = false;

        public int imageId { get; set; }

        [ForeignKey(nameof(imageId))]
        public Images image { get; set; }

        public ICollection<ProductStore> productStores { get; set; } = new List<ProductStore>();

        public ICollection<EntryOrderDetail> entryOrderDetails { get; set; } = new List<EntryOrderDetail>();

        public ICollection<ProductPrices> productPrices { get; set; } = new List<ProductPrices>();
    }
}
