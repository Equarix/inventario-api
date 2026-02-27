using invetario_api.Modules.products.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.payMethod.entity
{
    [Table("Paymethods")]
    public class Paymethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int paymethodId { get; set; }

        [Required]
        public string name { get; set; }

        public bool status { get; set; } = true;
    }
}
