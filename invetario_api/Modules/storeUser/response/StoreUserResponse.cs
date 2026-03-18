using System;
using invetario_api.Modules.store.response;
using invetario_api.Modules.storeUser.entity;
using invetario_api.Modules.users.response;

namespace invetario_api.Modules.storeUser.response;

public class StoreUserResponse
{
    public int StoreUserId { get; set; }

    public StoreSingleResponse Store { get; set; }

    public UserSingleResponse User { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool status { get; set; }


    public static StoreUserResponse fromEntity(Storeuser storeUser)
    {
        return new StoreUserResponse
        {
            StoreUserId = storeUser.StoreUserId,
            Store = StoreSingleResponse.fromEntity(storeUser.Store),
            User = UserSingleResponse.fromEntity(storeUser.User),
            CreatedAt = storeUser.CreatedAt,
            status = storeUser.status
        };
    }

    public static List<StoreUserResponse> fromEntityList(List<Storeuser> storeUsers)
    {
        return storeUsers.Select(storeUser => fromEntity(storeUser)).ToList();
    }
}


public class StoreUserResponseSingle
{
    public int StoreUserId { get; set; }


    public UserSingleResponse User { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool status { get; set; }


    public static StoreUserResponseSingle fromEntity(Storeuser storeUser)
    {
        return new StoreUserResponseSingle
        {
            StoreUserId = storeUser.StoreUserId,
            User = UserSingleResponse.fromEntity(storeUser.User),
            CreatedAt = storeUser.CreatedAt,
            status = storeUser.status
        };
    }

    public static List<StoreUserResponseSingle> fromEntityList(List<Storeuser> storeUsers)
    {
        return storeUsers.Select(storeUser => fromEntity(storeUser)).ToList();
    }
}