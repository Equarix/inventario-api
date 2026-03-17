using System;
using System.Security.Claims;
using invetario_api.database;
using invetario_api.Modules.users.entity;

namespace invetario_api.Modules.users.current_user;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly Database _db;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, Database db)
    {
        _httpContextAccessor = httpContextAccessor;
        _db = db;
    }

    public int? UserId
    {
        get
        {
            var userIdClaim = _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            return int.TryParse(userIdClaim, out var userId)
                ? userId
                : null;
        }
    }

    public async Task<User?> GetCurrentUser()
    {
        var userId = UserId;

        if (userId == null)
            return null;

        return await _db.users.FindAsync(userId);
    }
}
