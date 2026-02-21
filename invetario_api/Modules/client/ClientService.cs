using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.client.dto;
using invetario_api.Modules.client.entity;
using invetario_api.Modules.client.response;
using invetario_api.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.client
{
    public class ClientService : IClientService
    {
        private Database _db;

        public ClientService(Database db)
        {
            _db = db;
        }

        public async Task<List<ClientResponseSingle>> getClients()
        {
            var clients = await _db.clients.ToListAsync();
            return ClientResponseSingle.fromEntityList(clients);
        }

        public async Task<ClientResponseSingle> createClient(ClientDto data)
        {
            var newClient = new Client
            {
                clientType = data.clientType,
                name = data.name,
                typeDocument = data.typeDocument,
                documentNumber = data.documentNumber,
                phone = data.phone,
                email = data.email,
            };

            _db.clients.Add(newClient);
            await _db.SaveChangesAsync();

            return ClientResponseSingle.fromEntity(newClient);
        }

        public async Task<ClientResponseSingle?> deleteClient(int clientId)
        {
            var findClient = await _db.clients.Where(c => c.clientId == clientId).FirstOrDefaultAsync();
            if (findClient == null)
            {
                throw new HttpException(404, "Client not found");
            }

            findClient.status = false;
            await _db.SaveChangesAsync();
            return ClientResponseSingle.fromEntity(findClient);
        }

        public async Task<ClientResponseSingle?> getClientById(int clientId)
        {
            var findClient = await _db.clients.Where(c => c.clientId == clientId).FirstOrDefaultAsync();
            if (findClient == null)
            {
                throw new HttpException(404, "Client not found");
            }

            return ClientResponseSingle.fromEntity(findClient);
        }

        public async Task<ClientResponseSingle?> updateClient(int clientId, UpdateClientDto data)
        {
            var findClient = await _db.clients.Where(c => c.clientId == clientId).FirstOrDefaultAsync();
            if (findClient == null)
            {
                throw new HttpException(404, "Client not found");
            }

            findClient.clientType = data.clientType;
            findClient.name = data.name;
            findClient.typeDocument = data.typeDocument;
            findClient.documentNumber = data.documentNumber;
            findClient.phone = data.phone;
            findClient.email = data.email;
            findClient.status = data.status.Value;
            await _db.SaveChangesAsync();
            return ClientResponseSingle.fromEntity(findClient);
        }
    }
}
