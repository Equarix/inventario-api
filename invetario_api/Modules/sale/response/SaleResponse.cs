using System;
using invetario_api.Modules.client.response;
using invetario_api.Modules.store.response;
using invetario_api.Modules.users.response;

namespace invetario_api.Modules.sale.response;

public class SaleResponse
{
    public int saleId { get; set; }
    public ClientResponseSingle client { get; set; }
    public UserSingleResponse user { get; set; }
    public float total { get; set; }
    public string observations { get; set; }
    public List<SaleMethodsResponse> saleMethods { get; set; }
    public List<SaleDetailsResponse> saleDetails { get; set; }
    public DateTime createdAt { get; set; }
    public bool status { get; set; }
    public StoreSingleResponse store { get; set; }

    public static SaleResponse FromEntity(entity.Sale sale)
    {
        return new SaleResponse
        {
            saleId = sale.saleId,
            client = ClientResponseSingle.fromEntity(sale.client),
            user = UserSingleResponse.fromEntity(sale.user),
            total = sale.total,
            observations = sale.observations,
            saleMethods = SaleMethodsResponse.FromEntityList(sale.saleMethods.ToList()),
            saleDetails = SaleDetailsResponse.FromEntityList(sale.saleDetails.ToList()),
            createdAt = sale.createdAt,
            status = sale.status,
            store = StoreSingleResponse.fromEntity(sale.store)
        };
    }

    public static List<SaleResponse> FromEntityList(List<entity.Sale> sales)
    {
        return sales.Select(FromEntity).ToList();
    }
}
