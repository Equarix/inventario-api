using BCrypt.Net;
using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Jwt;
using invetario_api.Modules.box.entity;
using invetario_api.Modules.storeUser.entity;
using invetario_api.Modules.users.dto;
using invetario_api.Modules.users.entity;
using invetario_api.Modules.users.response;
using Microsoft.EntityFrameworkCore;

namespace invetario_api.Modules.users
{
    public class UserService : IUserService
    {
        private Database _db;

        public UserService(Database db)
        {
            _db = db;
        }

        public async Task<UserSingleResponse?> createUser(UserDto userDto)
        {
            var findEmail = _db.users.FirstOrDefault(u => u.email == userDto.email);

            if (findEmail != null)
            {
                throw new HttpException(409, "Email already Exists");
            }

            var hashPassword = BCrypt.Net.BCrypt.HashPassword(userDto.password);


            User newUser = new User
            {
                email = userDto.email,
                password = hashPassword,
                firstName = userDto.firstName,
                lastName = userDto.lastName,
                role = userDto.role ?? Role.AUDIENCE
            };

            var boxs = await _db.boxs
                .Where(b => userDto.boxIds.Contains(b.boxId))
                .ToListAsync();

            if (boxs.Count != userDto.boxIds.Count)
            {
                throw new HttpException(404, "One or more boxIds not found");
            }

            var stores = await _db.stores
                .Where(s => userDto.storeIds.Contains(s.storeId))
                .ToListAsync();

            if (stores.Count != userDto.storeIds.Count)
            {
                throw new HttpException(404, "One or more storeIds not found");
            }

            foreach (var store in stores)
            {

                Storeuser storeUser = new Storeuser
                {
                    Store = store,
                    User = newUser
                };
                newUser.storeUsers.Add(storeUser);
            }


            foreach (var box in boxs)
            {
                BoxUser boxUser = new BoxUser
                {
                    box = box,
                    user = newUser
                };
                newUser.boxUsers.Add(boxUser);
            }


            await _db.users.AddAsync(newUser);
            await _db.SaveChangesAsync();
            return UserSingleResponse.fromEntity(newUser);
        }


        public async Task<List<UserSingleResponse>> getUsers()
        {
            var users = await _db.users.ToListAsync();
            return UserSingleResponse.fromEntityList(users);
        }

        public async Task<UserLogin?> updateUser(int userId, UserDto userDto)
        {
            var user = await _db.users
                .Include(u => u.storeUsers)
                .ThenInclude(su => su.Store)
                .Include(u => u.boxUsers)
                .ThenInclude(bu => bu.box)
                .FirstOrDefaultAsync(u => u.userId == userId);

            if (user == null)
            {
                throw new HttpException(404, "User not found");
            }

            user.firstName = userDto.firstName;
            user.lastName = userDto.lastName;
            user.role = userDto.role ?? user.role;

            var boxs = await _db.boxs
                .Where(b => userDto.boxIds.Contains(b.boxId))
                .ToListAsync();

            if (boxs.Count != userDto.boxIds.Count)
            {
                throw new HttpException(404, "One or more boxIds not found");
            }

            var stores = await _db.stores
                .Where(s => userDto.storeIds.Contains(s.storeId))
                .ToListAsync();

            if (stores.Count != userDto.storeIds.Count)
            {
                throw new HttpException(404, "One or more storeIds not found");
            }

            user.storeUsers.Clear();
            user.boxUsers.Clear();

            foreach (var store in stores)
            {
                Storeuser storeUser = new Storeuser
                {
                    Store = store,
                    User = user
                };
                user.storeUsers.Add(storeUser);
            }

            foreach (var box in boxs)
            {
                BoxUser boxUser = new BoxUser
                {
                    box = box,
                    user = user
                };
                user.boxUsers.Add(boxUser);
            }

            await _db.SaveChangesAsync();
            return UserLogin.fromEntity(user);
        }
    }
}
