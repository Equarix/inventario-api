using invetario_api.Modules.client.response;
using invetario_api.Modules.store.response;
using invetario_api.Modules.users.response;

namespace invetario_api.Modules.proforma.response.builder;


public class ProformaResponseBuilder
{
    private int _proformaId;
    private UserSingleResponse? _user;
    private ClientResponseSingle? _client;
    private StoreSingleResponse? _store;
    private DateTime _createdAt;
    private ICollection<ProformaDetailsResponse>? _details;

    public ProformaResponseBuilder setProformaId(int proformaId)
    {
        _proformaId = proformaId;
        return this;
    }

    public ProformaResponseBuilder setUser(UserSingleResponse user)
    {
        _user = user;
        return this;
    }

    public ProformaResponseBuilder setClient(ClientResponseSingle client)
    {
        _client = client;
        return this;
    }

    public ProformaResponseBuilder setStore(StoreSingleResponse store)
    {
        _store = store;
        return this;
    }

    public ProformaResponseBuilder setCreatedAt(DateTime createdAt)
    {
        _createdAt = createdAt;
        return this;
    }

    public ProformaResponseBuilder setDetails(ICollection<ProformaDetailsResponse> details)
    {
        _details = details;
        return this;
    }

    public ProformaResponse build()
    {
        return new ProformaResponse()
        {
            proformaId = _proformaId,
            user = _user!,
            client = _client!,
            store = _store!,
            createdAt = _createdAt,
            details = _details!
        };
    }
}