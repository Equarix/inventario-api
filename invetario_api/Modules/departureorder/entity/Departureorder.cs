using invetario_api.Modules.client.entity;
using invetario_api.Modules.departureorder.enums;
using invetario_api.Modules.entryorder.enums;
using invetario_api.Modules.products.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.departureorder.entity
{
    [Table("Departureorders")]
    public class Departureorder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int departureorderId { get; set; }

        public DepartureType departureType { get; set; }

        public int clientId { get; set; }

        [ForeignKey(nameof(clientId))]
        public Client client { get; set; }

        public DateTime departureDate { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;

        public string motive { get; set; }

        public EntryOrderStatus status { get; set; } = EntryOrderStatus.PENDING;

        public float tax { get; set; }

        public string observations { get; set; }

        public string documentReference { get; set; }
    }
}
