using System;
using invetario_api.Modules.client.entity;

namespace invetario_api.Modules.client.response;

public class ClientResponseSingle
{
    public int clientId { get; set; }
    public ClientType clientTypeId { get; set; }
    public string clientType { get; set; }
    public string name { get; set; }
    public string typeDocument { get; set; }
    public string documentNumber { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public bool status { get; set; }
    public DateTime createdAt { get; set; }


    public static ClientResponseSingle fromEntity(Client client)
    {
        return new ClientResponseSingle
        {
            clientId = client.clientId,
            clientTypeId = client.clientType,
            clientType = client.clientType.ToString(),
            name = client.name,
            typeDocument = client.typeDocument,
            documentNumber = client.documentNumber,
            phone = client.phone,
            email = client.email,
            status = client.status,
            createdAt = client.createdAt
        };
    }

    public static List<ClientResponseSingle> fromEntityList(List<Client> clients)
    {
        List<ClientResponseSingle> responseList = new List<ClientResponseSingle>();
        foreach (var client in clients)
        {
            responseList.Add(fromEntity(client));
        }
        return responseList;
    }
}
