using invetario_api.Modules.products.entity;
using invetario_api.Modules.users.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.box.entity
{
    [Table("Boxs")]
    public class Box
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int boxId { get; set; }

        public DateTime dateOpening { get; set; } = DateTime.UtcNow;

        public DateTime? dateClosing { get; set; }

        public decimal amountOpening { get; set; }

        public decimal? amountClosing { get; set; }

        [Required]
        public int userOpeningId { get; set; }

        [ForeignKey(nameof(userOpeningId))]
        public User userOpening { get; set; }

        public int? userClosingId { get; set; }

        [ForeignKey(nameof(userClosingId))]
        public User? userClosing { get; set; }

        [Required]
        public int userActualId { get; set; }

        [ForeignKey(nameof(userActualId))]
        public User userActual { get; set; }

        public bool isOpen { get; set; } = true;
    }
}
