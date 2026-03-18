using invetario_api.Modules.storeUser.dto;
using invetario_api.Modules.storeUser.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.storeUser
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StoreuserController : ControllerBase
    {
        private IStoreuserService _storeUserService;

        public StoreuserController(IStoreuserService storeUserService)
        {
            _storeUserService = storeUserService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var result = await _storeUserService.getStoreusers();
            return Ok(result);
        }

        [HttpGet("by-token")]
        public async Task<IActionResult> FindByToken()
        {
            var result = await _storeUserService.getStoreByToken();
            return Ok(result);
        }

        [HttpGet("users-by-store/{storeId:int}")]
        public async Task<IActionResult> GetUsersByStoreId(int storeId)
        {
            var result = await _storeUserService.getUsersByStoreId(storeId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StoreuserDto data)
        {
            var result = await _storeUserService.createStoreuser(data);
            return Ok(result);
        }


        [HttpDelete("{storeUserId:int}")]
        public async Task<IActionResult> delete(int storeUserId)
        {
            var result = await _storeUserService.deleteStoreuser(storeUserId);
            return Ok(result);
        }
    }
}
