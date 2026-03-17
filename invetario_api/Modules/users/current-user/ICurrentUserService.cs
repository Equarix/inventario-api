using System;
using invetario_api.Modules.users.entity;

namespace invetario_api.Modules.users.current_user;

public interface ICurrentUserService
{
    int? UserId { get; }

    public Task<User?> GetCurrentUser();
}
