using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.provider.dto;
using invetario_api.Modules.provider.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.provider
{
    public class ProviderService : IProviderService
    {
        private Database _db;

        public ProviderService(Database db)
        {
            _db = db;
        }

        public async Task<List<Provider>> getProviders()
        {
            return await _db.providers.ToListAsync();
        }

        public async Task<Provider> createProvider(ProviderDto data)
        {
            var newProvider = new Provider
            {
                code = data.code,
                companyName = data.companyName,
                publicName = data.publicName,
                typeDocument = data.typeDocument,
                documentNumber = data.documentNumber,
                address = data.address,
                phone = data.phone,
                email = data.email,
                mainContact = data.mainContact,
                contactPhone = data.contactPhone,
                payCondition = data.payCondition,
                typeMoney = data.typeMoney,
                daysDelivery = data.daysDelivery,
            };

            _db.providers.Add(newProvider);
            await _db.SaveChangesAsync();

            return newProvider;
        }

        public async Task<Provider?> deleteProvider(int providerId)
        {
            var provider = await _db.providers.Where(p => p.providerId == providerId).FirstOrDefaultAsync();
            if (provider == null)
            {
                throw new HttpException(404, "Provider not found");
            }

            provider.status = false;
            await _db.SaveChangesAsync();
            return provider;
        }

        public async Task<Provider?> getProviderById(int providerId)
        {
            var provider = await _db.providers.Where(p => p.providerId == providerId).FirstOrDefaultAsync();
            if (provider == null)
            {
                throw new HttpException(404, "Provider not found");
            }

            return provider;
        }

        public async Task<Provider?> updateProvider(int providerId, UpdateProviderDto data)
        {
            var provider = await _db.providers.Where(p => p.providerId == providerId).FirstOrDefaultAsync();
            if (provider == null)
            {
                throw new HttpException(404, "Provider not found");
            }

            provider.code = data.code;
            provider.companyName = data.companyName;
            provider.publicName = data.publicName;
            provider.typeDocument = data.typeDocument;
            provider.documentNumber = data.documentNumber;
            provider.address = data.address;
            provider.phone = data.phone;
            provider.email = data.email;
            provider.mainContact = data.mainContact;
            provider.contactPhone = data.contactPhone;
            provider.payCondition = data.payCondition;
            provider.typeMoney = data.typeMoney;
            provider.daysDelivery = data.daysDelivery;
            provider.status = data.status!.Value;
            await _db.SaveChangesAsync();

            return provider;
        }
    }
}
