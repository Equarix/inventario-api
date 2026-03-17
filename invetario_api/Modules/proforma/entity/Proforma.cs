using invetario_api.Modules.client.entity;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.store.entity;
using invetario_api.Modules.users.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.proforma.entity
{
    [Table("Proformas")]
    public class Proforma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int proformaId { get; set; }

        [Required]
        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public User user { get; set; }

        [Required]
        public int clientId { get; set; }

        [ForeignKey(nameof(clientId))]
        public Client client { get; set; }

        [Required]
        public int storeId { get; set; }

        [ForeignKey(nameof(storeId))]
        public Store store { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;

        public ICollection<ProformaDetails> details { get; set; } = new List<ProformaDetails>();
    }
}
