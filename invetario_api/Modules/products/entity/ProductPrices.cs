using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.products.entity;

[Table("ProductPrices")]
public class ProductPrices
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int productPriceId { get; set; }


    [Required]
    public int productId { get; set; }

    [ForeignKey(nameof(productId))]
    public Product product { get; set; }


    [Required]
    public float price { get; set; }

    public DateTime createdAt { get; set; } = DateTime.Now;

    [Required]
    public Boolean status { get; set; } = true;
}
