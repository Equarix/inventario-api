using invetario_api.Modules.storeUser.dto;
using invetario_api.Modules.storeUser.entity;
using invetario_api.Modules.storeUser.response;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.storeUser
{
    public interface IStoreuserService
    {
        Task<List<StoreUserResponse>> getStoreusers();

        Task<StoreUserResponse> createStoreuser(StoreuserDto data);

        Task<StoreUserResponse?> deleteStoreuser(int storeUserId);

        Task<List<StoreUserResponse>> getStoreByToken();

        Task<List<StoreUserResponseSingle>> getUsersByStoreId(int storeId);
    }
}
