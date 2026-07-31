using invetario_api.Modules.box.dto;
using invetario_api.Modules.box.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using invetario_api.Utils;


namespace invetario_api.Modules.box
{

    [ApiController]
    [Route("api/[controller]")]
    public class BoxController : ControllerBase
    {
        private IBoxService _boxService;

        public BoxController(IBoxService boxService)
        {
            _boxService = boxService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll([FromQuery] PaginateDto paginate)
        {
            var result = await _boxService.getBoxs(paginate);
            return Ok(result);
        }

        [HttpGet("{boxId:int}")]
        public async Task<IActionResult> FindById(int boxId)
        {
            var result = await _boxService.getBoxById(boxId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BoxDto data)
        {
            var result = await _boxService.createBox(data);
            return Ok(result);
        }

        [HttpPut("{boxId:int}")]
        public async Task<IActionResult> update(int boxId, [FromBody] UpdateBoxDto data)
        {
            var result = await _boxService.updateBox(boxId, data);
            return Ok(result);
        }


        [HttpDelete("{boxId:int}")]
        public async Task<IActionResult> delete(int boxId)
        {
            var result = await _boxService.deleteBox(boxId);
            return Ok(result);
        }
    }
}
