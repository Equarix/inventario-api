using invetario_api.Modules.auth.dto;
using invetario_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.auth
{

    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login([FromBody] LoginDto loginDto)
        {
            var loginResponse = await _authService.login(loginDto);
            return Ok(loginResponse);
        }
    }
}
