using System;
using invetario_api.Modules.box.response;
using invetario_api.Modules.store.response;
using invetario_api.Modules.users.entity;

namespace invetario_api.Modules.users.response;

public class UserLogin
{
    public int userId { get; set; }
    public string email { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public Role role { get; set; } = Role.AUDIENCE;
    public bool status { get; set; } = true;

    public List<StoreSingleResponse> stores { get; set; }

    public List<BoxSingleResponse> boxes { get; set; }

    public static UserLogin fromEntity(User user)
    {
        return new UserLogin
        {
            userId = user.userId,
            email = user.email,
            firstName = user.firstName,
            lastName = user.lastName,
            role = user.role,
            status = user.status,
            stores = user.storeUsers.Select(su => StoreSingleResponse.fromEntity(su.Store)).ToList(),
            boxes = user.boxUsers.Select(bu => BoxSingleResponse.fromEntity(bu.box)).ToList(),
        };
    }

    public static List<UserLogin> fromEntityList(List<User> users)
    {
        return users.Select(user => fromEntity(user)).ToList();
    }
}
