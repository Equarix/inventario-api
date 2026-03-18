using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.users.entity;

namespace invetario_api.Modules.store.entity;

[Table("report_product")]
public class ReportProduct
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int reportProductId { get; set; }

    [Required]
    public int productId { get; set; }

    [ForeignKey(nameof(productId))]
    public Product product { get; set; }

    [Required]
    public int quantity { get; set; }

    [Required]
    public DateTime date { get; set; } = DateTime.Now;

    [Required]
    public int storeId { get; set; }

    [ForeignKey(nameof(storeId))]
    public Store store { get; set; }

    [Required]
    public int userId { get; set; }

    [ForeignKey(nameof(userId))]
    public User user { get; set; }
}
