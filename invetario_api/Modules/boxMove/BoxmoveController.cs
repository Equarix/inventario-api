using invetario_api.Modules.boxMove.dto;
using invetario_api.Modules.boxMove.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.boxMove
{

    [ApiController]
    [Route("api/[controller]")]
    public class BoxmoveController : ControllerBase
    {
        private IBoxmoveService _boxMoveService;

        public BoxmoveController(IBoxmoveService boxMoveService) {
            _boxMoveService = boxMoveService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll() 
        {
            var result = await _boxMoveService.getBoxmoves();
            return Ok(result);
        }
        
        [HttpGet("{boxMoveId:int}")]
        public async Task<IActionResult> FindById(int boxMoveId) 
        {
            var result = await _boxMoveService.getBoxmoveById(boxMoveId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BoxmoveDto data)
        {
            var result = await _boxMoveService.createBoxmove(data);
            return Ok(result);
        }

        [HttpPut("{boxMoveId:int}")]
        public async Task<IActionResult> update(int boxMoveId, [FromBody] UpdateBoxmoveDto data)
        {            
            var result = await _boxMoveService.updateBoxmove(boxMoveId, data);
            return Ok(result);
        }


        [HttpDelete("{boxMoveId:int}")]
        public async Task<IActionResult> delete(int boxMoveId)
        {
            var result = await _boxMoveService.deleteBoxmove(boxMoveId);
            return Ok(result);
        }
    }
}
