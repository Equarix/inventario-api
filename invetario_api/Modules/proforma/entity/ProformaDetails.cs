using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using invetario_api.Modules.products.entity;

namespace invetario_api.Modules.proforma.entity;

[Table("ProformaDetails")]
public class ProformaDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int proformaDetailsId { get; set; }

    public int proformaId { get; set; }

    [ForeignKey(nameof(proformaId))]
    public Proforma proforma { get; set; }

    public int productId { get; set; }

    [ForeignKey(nameof(productId))]
    public Product product { get; set; }

    public int quantity { get; set; }

    public string productName { get; set; }

    public decimal price { get; set; }
}
