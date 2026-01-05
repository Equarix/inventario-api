using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using invetario_api.Modules.entryorder.enums;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.store.entity;

namespace invetario_api.Modules.departureorder.entity;

[Table("DepartureOrderDetails")]
public class DepartureOrderDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DepartureOrderDetailId { get; set; }

    public int DepartureOrderId { get; set; }

    [ForeignKey(nameof(DepartureOrderId))]
    public Departureorder Departureorder { get; set; }

    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }

    public int storeId { get; set; }

    [ForeignKey(nameof(storeId))]
    public Store Store { get; set; }

    public string unitType { get; set; }

    public int quantity { get; set; }

    public int departedQuantity { get; set; }

    public float unitPrice { get; set; }

    public string lote { get; set; }

    public EntryOrderStatus status { get; set; } = EntryOrderStatus.PENDING;

    public DateTime createdAt { get; set; } = DateTime.Now;
}
