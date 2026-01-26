using invetario_api.database;
using invetario_api.Exceptions;
using invetario_api.Jwt;
using invetario_api.Modules.auth.dto;
using invetario_api.Modules.auth.response;
using invetario_api.Modules.users.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.auth
{
    public class AuthService : IAuthService
    {
        private Database _db;
        private JwtUtils _jwt;

        public AuthService(Database db, JwtUtils jwt)
        {
            _db = db;
            _jwt = jwt;
        }


        public async Task registerAdmin()
        {
            var adminEmail = "admin@gmail.com";
            var adminPassword = "123";
            var existingAdmin = await _db.users
                .Where(u => u.email == adminEmail)
                .FirstOrDefaultAsync();
            if (existingAdmin != null)
            {
                return;
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(adminPassword);
            var adminUser = new User
            {
                email = adminEmail,
                password = hashedPassword,
                role = Role.ADMIN,
                firstName = "Admin",
                lastName = "User",
            };

            _db.users.Add(adminUser);
            await _db.SaveChangesAsync();
        }


        public async Task<LoginResponse?> login(LoginDto loginDto)
        {
            var findUser = await _db.users
                .Where(u => u.email == loginDto.email)
                .FirstOrDefaultAsync();

            if (findUser == null)
            {
                throw new HttpException(401, "Invalid email or password");
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.password, findUser.password);

            if (!isPasswordValid)
            {
                throw new HttpException(401, "Invalid email or password");
            }


            var token = _jwt.generateJwt(findUser);

            return new LoginResponse
            {
                token = token,
                user = findUser
            };
        }
    }
}
