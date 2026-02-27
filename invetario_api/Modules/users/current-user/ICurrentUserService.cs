using System;

namespace invetario_api.Modules.users.current_user;

public interface ICurrentUserService
{
    int? UserId { get; }
}
