using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Modules.storeUser.dto;
using invetario_api.Modules.storeUser.entity;
using invetario_api.Modules.storeUser.response;
using invetario_api.Modules.users.current_user;
using invetario_api.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.storeUser
{
    public class StoreuserService : IStoreuserService
    {
        private Database _db;
        private readonly ICurrentUserService _currentUser;

        public StoreuserService(Database db, ICurrentUserService currentUser)
        {
            _db = db;
            _currentUser = currentUser;
        }

        public async Task<List<StoreUserResponse>> getStoreusers()
        {
            var storeUsers = await _db.storeUsers
                .Include(su => su.Store)
                .Include(su => su.User)
                .ToListAsync();
            return StoreUserResponse.fromEntityList(storeUsers);

        }

        public async Task<StoreUserResponse> createStoreuser(StoreuserDto data)
        {
            var store = await _db.stores.FindAsync(data.storeId);
            if (store == null) throw new HttpException(StatusCodes.Status404NotFound, "Store not found");

            var user = await _db.users.FindAsync(data.userId);
            if (user == null) throw new HttpException(StatusCodes.Status404NotFound, "User not found");

            var storeUser = new Storeuser
            {
                StoreId = data.storeId!.Value,
                UserId = data.userId!.Value
            };

            _db.storeUsers.Add(storeUser);
            await _db.SaveChangesAsync();

            return StoreUserResponse.fromEntity(storeUser);
        }

        public async Task<StoreUserResponse?> deleteStoreuser(int storeUserId)
        {
            var storeUser = await _db.storeUsers.Include(su => su.Store).Include(su => su.User)
                .Where(su => su.StoreUserId == storeUserId).FirstOrDefaultAsync();
            if (storeUser == null) throw new HttpException(StatusCodes.Status404NotFound, "StoreUser not found");

            storeUser.status = false;
            await _db.SaveChangesAsync();

            return StoreUserResponse.fromEntity(storeUser);
        }

        public Task<List<StoreUserResponse>> getStoreByToken()
        {
            var userId = _currentUser.UserId;

            var storeUsers = _db.storeUsers
                .Include(su => su.Store)
                .Include(su => su.User)
                .Where(su => su.UserId == userId && su.status == true)
                .ToList();

            return Task.FromResult(StoreUserResponse.fromEntityList(storeUsers));
        }

        public async Task<List<StoreUserResponseSingle>> getUsersByStoreId(int storeId)
        {
            var storesUsers = await _db.storeUsers
                .Include(su => su.User)
                .Where(su => su.StoreId == storeId && su.status == true)
                .ToListAsync();

            return StoreUserResponseSingle.fromEntityList(storesUsers);
        }
    }
}
