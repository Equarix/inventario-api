using invetario_api.Modules.auth.dto;
using invetario_api.Modules.auth.response;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.auth
{
    public interface IAuthService
    {
        public Task<LoginResponse?> login(LoginDto loginDto);
        public Task registerAdmin();
    }
}
