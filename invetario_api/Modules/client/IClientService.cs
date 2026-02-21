using invetario_api.Modules.client.dto;
using invetario_api.Modules.client.entity;
using invetario_api.Modules.client.response;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.client
{
    public interface IClientService
    {
        Task<List<ClientResponseSingle>> getClients();

        Task<ClientResponseSingle?> getClientById(int clientId);

        Task<ClientResponseSingle> createClient(ClientDto data);

        Task<ClientResponseSingle?> updateClient(int clientId, UpdateClientDto data);

        Task<ClientResponseSingle?> deleteClient(int clientId);
    }
}
