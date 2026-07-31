using invetario_api.Modules.dayBox.entity;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.store.entity;
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

        public string boxName { get; set; }

        public string serie { get; set; }

        public string serieProforma { get; set; }

        public int storeId { get; set; }

        public bool status { get; set; } = true;

        [ForeignKey("storeId")]
        public Store store { get; set; }

        public ICollection<Daybox> dayboxes { get; set; }

        public ICollection<BoxUser> boxUsers { get; set; }
    }
}
