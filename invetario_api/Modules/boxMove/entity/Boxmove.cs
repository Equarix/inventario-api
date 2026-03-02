using invetario_api.Modules.box.entity;
using invetario_api.Modules.payMethod.entity;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.users.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.boxMove.entity
{
    [Table("Boxmoves")]
    public class Boxmove
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int boxMoveId { get; set; }

        [Required]
        public int boxId { get; set; }

        [ForeignKey(nameof(boxId))]
        public Box box { get; set; }

        [Required]
        public decimal quantity { get; set; }

        [Required]
        public DateTime dateMove { get; set; }

        [Required]
        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public User user { get; set; }

        [Required]
        public int paymentMethodId { get; set; }

        [ForeignKey(nameof(paymentMethodId))]
        public Paymethod paymentMethod { get; set; }
    }
}
