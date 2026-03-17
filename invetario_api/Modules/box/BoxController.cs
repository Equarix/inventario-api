using invetario_api.Modules.box.dto;
using invetario_api.Modules.box.entity;
using invetario_api.Modules.users.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.box
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BoxController : ControllerBase
    {
        private IBoxService _boxService;

        public BoxController(IBoxService boxService)
        {
            _boxService = boxService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var result = await _boxService.getBoxs();
            return Ok(result);
        }

        [HttpGet("open")]
        public async Task<IActionResult> FindOpenBoxByToken()
        {
            var result = await _boxService.getOpenBoxByToken();
            return Ok(result);
        }

        [HttpGet("{boxId:int}")]
        public async Task<IActionResult> FindById(int boxId)
        {
            var result = await _boxService.getBoxById(boxId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> openBox([FromBody] BoxDto data)
        {
            var result = await _boxService.openBox(data);
            return Ok(result);
        }


        [HttpDelete("{boxId:int}")]
        public async Task<IActionResult> closeBox(int boxId)
        {
            var result = await _boxService.closeBox(boxId);
            return Ok(result);
        }
    }
}
