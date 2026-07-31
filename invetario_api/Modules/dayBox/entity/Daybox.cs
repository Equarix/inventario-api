using invetario_api.Modules.box.entity;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.users.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.dayBox.entity
{
    [Table("Dayboxs")]
    public class Daybox
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int dayboxId { get; set; }

        public DateTime date { get; set; }

        public int boxId { get; set; }

        public bool status { get; set; } = true;


        public float totalefectivo { get; set; }

        public float totalTarjeta { get; set; }

        public string observations { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;

        [ForeignKey("boxId")]
        public Box box { get; set; }

        public int userId { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }
    }
}
