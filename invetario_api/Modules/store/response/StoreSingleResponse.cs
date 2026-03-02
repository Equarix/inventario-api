using System;
using invetario_api.Modules.store.entity;

namespace invetario_api.Modules.store.response;

public class StoreSingleResponse
{
    public int storeId { get; set; }
    public string name { get; set; }
    public string code { get; set; }
    public string address { get; set; }
    public string phone { get; set; }
    public int maxCapacity { get; set; }
    public bool status { get; set; }
    public string type { get; set; }
    public DateTime createdAt { get; set; } = DateTime.Now;
    public string observations { get; set; }

    public static StoreSingleResponse fromEntity(Store store)
    {
        return new StoreSingleResponse
        {
            storeId = store.storeId,
            name = store.name,
            code = store.code,
            address = store.address,
            phone = store.phone,
            maxCapacity = store.maxCapacity,
            status = store.status,
            type = store.type,
            createdAt = store.createdAt,
            observations = store.observations
        };
    }

    public static List<StoreSingleResponse> fromEntityList(List<Store> stores)
    {
        return stores.Select(store => fromEntity(store)).ToList();
    }
}