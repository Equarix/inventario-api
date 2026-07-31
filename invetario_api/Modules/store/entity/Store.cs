using invetario_api.Modules.box.entity;
using invetario_api.Modules.entryorder.entity;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.users.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.store.entity
{
    [Table("Stores")]
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int storeId { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string code { get; set; }

        [Required]
        public string address { get; set; }


        [Required]
        [MaxLength(9)]
        public string phone { get; set; }

        [Required]
        public int maxCapacity { get; set; }

        public bool status { get; set; } = true;

        public string type { get; set; } = StoredType.PRINCIPAL;

        [Required]
        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public User user { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;

        public string observations { get; set; }

        public ICollection<ProductStore> productStores { get; set; } = new List<ProductStore>();

        public ICollection<Entryorder> entryOrders { get; set; } = new List<Entryorder>();

        public ICollection<Box> boxs { get; set; } = new List<Box>();
    }
}
