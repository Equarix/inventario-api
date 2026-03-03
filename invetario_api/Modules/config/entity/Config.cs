using invetario_api.Modules.products.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.config.entity
{
    [Table("Configs")]
    public class Config
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int configId { get; set; }

        [Required]
        public string enterpriseName { get; set; }

        [Required]
        public string contactEmail { get; set; }

        [Required]
        public string ruc { get; set; }

        public string address { get; set; }

        public string phone { get; set; }

        public string logoUrl { get; set; }

        public string localCurrency { get; set; }


        [Required]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
