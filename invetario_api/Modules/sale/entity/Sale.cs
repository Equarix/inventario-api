using invetario_api.Modules.client.entity;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.store.entity;
using invetario_api.Modules.users.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.sale.entity
{
    [Table("Sales")]
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int saleId { get; set; }

        [Required]
        public int clientId { get; set; }

        [ForeignKey(nameof(clientId))]
        public Client client { get; set; }

        [Required]
        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public User user { get; set; }

        [Required]
        public float total { get; set; }

        public string observations { get; set; }

        public ICollection<SaleMethods> saleMethods { get; set; } = new List<SaleMethods>();

        public ICollection<SaleDetails> saleDetails { get; set; } = new List<SaleDetails>();

        public DateTime createdAt { get; set; } = DateTime.Now;

        public bool status { get; set; } = true;

        [Required]
        public int storeId { get; set; }

        [ForeignKey(nameof(storeId))]
        public Store store { get; set; }
    }
}
