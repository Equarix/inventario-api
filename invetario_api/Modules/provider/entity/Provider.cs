using invetario_api.Modules.products.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.provider.entity
{
    [Table("Providers")]
    public class Provider
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int providerId { get; set; }
        public string code { get; set; }
        public string companyName { get; set; }
        public string publicName { get; set; }
        public string typeDocument { get; set; }
        public string documentNumber { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string mainContact { get; set; }
        public string contactPhone { get; set; }
        public PayCondition payCondition { get; set; }
        public string typeMoney { get; set; }
        public int daysDelivery { get; set; }
        public bool status { get; set; } = true;
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
