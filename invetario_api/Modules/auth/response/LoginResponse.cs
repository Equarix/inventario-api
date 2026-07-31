using invetario_api.Modules.users.entity;
using invetario_api.Modules.users.response;

namespace invetario_api.Modules.auth.response
{
    public class LoginResponse
    {
        public string token { get; set; }

        public UserLogin user { get; set; }


    }
}
