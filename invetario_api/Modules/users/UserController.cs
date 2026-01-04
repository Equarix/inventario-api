using invetario_api.Modules.users.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace invetario_api.Modules.users
{
    [ApiController]
    [Authorize(Roles = RoleString.Admin)]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.getUsers();
            return Ok(users);
        }



        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] dto.UserDto userDto)
        {
            var newUser = await _userService.createUser(userDto);
            return Ok(newUser);
        }

    }
}
