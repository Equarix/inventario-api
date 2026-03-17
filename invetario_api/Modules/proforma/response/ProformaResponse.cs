using System;
using invetario_api.Modules.client.response;
using invetario_api.Modules.proforma.entity;
using invetario_api.Modules.store.response;
using invetario_api.Modules.users.response;

namespace invetario_api.Modules.proforma.response;

public class ProformaResponse
{
    public int proformaId { get; set; }


    public UserSingleResponse user { get; set; }

    public ClientResponseSingle client { get; set; }

    public StoreSingleResponse store { get; set; }

    public DateTime createdAt { get; set; } = DateTime.Now;

    public ICollection<ProformaDetailsResponse> details { get; set; } = new List<ProformaDetailsResponse>();

    public static ProformaResponse fromProforma(Proforma proforma)
    {
        return new ProformaResponse()
        {
            proformaId = proforma.proformaId,
            user = UserSingleResponse.fromEntity(proforma.user),
            client = ClientResponseSingle.fromEntity(proforma.client),
            store = StoreSingleResponse.fromEntity(proforma.store),
            createdAt = proforma.createdAt,
            details = ProformaDetailsResponse.fromProformaDetailsList(proforma.details)
        };
    }

    public static List<ProformaResponse> fromProformaList(List<Proforma> proformas)
    {
        return proformas.Select(x => fromProforma(x)).ToList();
    }
}
