using invetario_api.Modules.products.entity;
using invetario_api.Modules.store.entity;
using invetario_api.Modules.users.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.storeUser.entity
{
    [Table("Storeusers")]
    public class Storeuser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreUserId { get; set; }

        [Required]
        public int StoreId { get; set; }

        [ForeignKey(nameof(StoreId))]

        public Store Store { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool status { get; set; } = true;
    }
}
