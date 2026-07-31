using invetario_api.Modules.dayBox.dto;
using invetario_api.Modules.dayBox.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using invetario_api.Utils;


namespace invetario_api.Modules.dayBox
{

    [ApiController]
    [Route("api/[controller]")]
    public class DayboxController : ControllerBase
    {
        private IDayboxService _dayBoxService;

        public DayboxController(IDayboxService dayBoxService) {
            _dayBoxService = dayBoxService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll([FromQuery] PaginateDto paginate) 
        {
            var result = await _dayBoxService.getDayboxs(paginate);
            return Ok(result);
        }
        
        [HttpGet("{dayBoxId:int}")]
        public async Task<IActionResult> FindById(int dayBoxId) 
        {
            var result = await _dayBoxService.getDayboxById(dayBoxId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DayboxDto data)
        {
            var result = await _dayBoxService.createDaybox(data);
            return Ok(result);
        }

        [HttpPut("{dayBoxId:int}")]
        public async Task<IActionResult> update(int dayBoxId, [FromBody] UpdateDayboxDto data)
        {            
            var result = await _dayBoxService.updateDaybox(dayBoxId, data);
            return Ok(result);
        }


        [HttpDelete("{dayBoxId:int}")]
        public async Task<IActionResult> delete(int dayBoxId)
        {
            var result = await _dayBoxService.deleteDaybox(dayBoxId);
            return Ok(result);
        }
    }
}
