using invetario_api.Modules.users.dto;
using invetario_api.Modules.users.response;

namespace invetario_api.Modules.users
{
    public interface IUserService
    {
        public Task<UserSingleResponse?> createUser(UserDto userDto);

        public Task<List<UserSingleResponse>> getUsers();
    }
}
