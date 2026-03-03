using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using invetario_api.Modules.payMethod.entity;

namespace invetario_api.Modules.sale.entity;

[Table("SaleMethods")]
public class SaleMethods
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int saleMethodId { get; set; }

    [Required]
    public string methodPayment { get; set; }

    [Required]
    public int saleId { get; set; }

    [ForeignKey(nameof(saleId))]
    public Sale sale { get; set; }

    [Required]
    public float amount { get; set; }

    [Required]
    public int payMethodId { get; set; }

    [ForeignKey(nameof(payMethodId))]
    public Paymethod payMethod { get; set; }
}
