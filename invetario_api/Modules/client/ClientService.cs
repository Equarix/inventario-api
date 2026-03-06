using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.client.dto;
using invetario_api.Modules.client.entity;
using invetario_api.Modules.client.response;
using invetario_api.utils;
using invetario_api.Utils;
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

        public async Task<List<ClientResponseSingle>> getClientsV1()
        {
            var clients = await _db.clients.ToListAsync();
            return ClientResponseSingle.fromEntityList(clients);
        }

        public async Task<PageResult<List<ClientResponseSingle>>> getClients(PaginateDto paginate)
        {
            var query = _db.clients.AsQueryable();

            var totalItems = await query.CountAsync();

            var clients = await query
                .Skip((paginate.page - 1) * paginate.limit)
                .Take(paginate.limit)
                .ToListAsync();

            return new PageResult<List<ClientResponseSingle>>
            {
                items = ClientResponseSingle.fromEntityList(clients),
                totalItems = totalItems,
                page = paginate.page,
                limit = paginate.limit
            };
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

        public async Task<List<ClientResponseSingle>> searchClients(string documentNumber)
        {
            var clients = await _db.clients.Where(c => c.documentNumber.Contains(documentNumber)).ToListAsync();
            return ClientResponseSingle.fromEntityList(clients);
        }
    }
}
