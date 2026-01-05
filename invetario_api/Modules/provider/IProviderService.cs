using invetario_api.Modules.provider.dto;
using invetario_api.Modules.provider.entity;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.provider
{
    public interface IProviderService
    {
        Task<List<Provider>> getProviders();

        Task<Provider?> getProviderById(int providerId);
        
        Task<Provider> createProvider(ProviderDto data);

        Task<Provider?> updateProvider(int providerId, UpdateProviderDto data);

        Task<Provider?> deleteProvider(int providerId);
    }
}
