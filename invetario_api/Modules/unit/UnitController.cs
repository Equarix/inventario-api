using invetario_api.Modules.unit.dto;
using invetario_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace invetario_api.Modules.unit
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitController : ControllerBase
    {
        private IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FindAll()
        {
            var result = await _unitService.getUnits();
            return Ok(result);
        }


        [HttpGet("{unitId:int}")]
        [Authorize(Roles = "ADMIN,STORE")]
        public async Task<IActionResult> FindById(int unitId)
        {
            var result = await _unitService.getUnitById(unitId);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN,STORE")]
        public async Task<IActionResult> Create([FromBody] UnitDto unitDto)
        {
            var result = await _unitService.createUnit(unitDto);
            return Ok(result);
        }

        [HttpPut("{unitId:int}")]
        [Authorize(Roles = "ADMIN,STORE")]
        public async Task<IActionResult> Update(int unitId, [FromBody] UpdateUnitDto unitDto)
        {
            var result = await _unitService.updateUnit(unitId, unitDto);

            return Ok(result);
        }


        [HttpDelete("{unitId:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int unitId)
        {
            var result = await _unitService.deleteUnit(unitId);

            return Ok(result);
        }
    }
}
