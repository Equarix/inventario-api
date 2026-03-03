using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using invetario_api.Modules.products.entity;

namespace invetario_api.Modules.sale.entity;

[Table("SaleDetails")]
public class SaleDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int saleDetailId { get; set; }

    [Required]
    public int saleId { get; set; }

    [ForeignKey(nameof(saleId))]
    public Sale sale { get; set; }


    [Required]
    public int productId { get; set; }

    [ForeignKey(nameof(productId))]
    public Product product { get; set; }

    [Required]
    public string productName { get; set; }

    [Required]
    public int quantity { get; set; }

    [Required]
    public decimal priceSell { get; set; }
}
