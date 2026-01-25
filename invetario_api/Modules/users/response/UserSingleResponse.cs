using invetario_api.Modules.users.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.users.response
{
    public class UserSingleResponse
    {
        public int userId { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Role role { get; set; } = Role.AUDIENCE;
        public bool status { get; set; } = true;

        public static UserSingleResponse fromEntity(User user)
        {
            return new UserSingleResponse
            {
                userId = user.userId,
                email = user.email,
                firstName = user.firstName,
                lastName = user.lastName,
                role = user.role,
                status = user.status
            };
        }

        public static List<UserSingleResponse> fromEntityList(List<User> users)
        {
            return users.Select(user => fromEntity(user)).ToList();
        }
    }
}
