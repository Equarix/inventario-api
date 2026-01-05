using invetario_api.Modules.provider.dto;
using invetario_api.Modules.provider.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.provider
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FindAll()
        {
            var result = await _providerService.getProviders();
            return Ok(result);
        }

        [HttpGet("{providerId:int}")]
        [Authorize]
        public async Task<IActionResult> FindById(int providerId)
        {
            var result = await _providerService.getProviderById(providerId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] ProviderDto data)
        {
            var result = await _providerService.createProvider(data);
            return Ok(result);
        }

        [HttpPut("{providerId:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> update(int providerId, [FromBody] UpdateProviderDto data)
        {
            var result = await _providerService.updateProvider(providerId, data);
            return Ok(result);
        }


        [HttpDelete("{providerId:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> delete(int providerId)
        {
            var result = await _providerService.deleteProvider(providerId);
            return Ok(result);
        }
    }
}
