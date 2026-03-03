using invetario_api.Modules.config.dto;
using Microsoft.AspNetCore.Mvc;


namespace invetario_api.Modules.config
{

    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase
    {
        private IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var result = await _configService.getConfigs();
            return Ok(result);
        }

        [HttpGet("last")]
        public async Task<IActionResult> GetLast()
        {
            var result = await _configService.getLast();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConfigDto data)
        {
            var result = await _configService.createConfig(data);
            return Ok(result);
        }
    }
}
