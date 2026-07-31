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

        public DayboxController(IDayboxService dayBoxService)
        {
            _dayBoxService = dayBoxService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll([FromQuery] QueryDayBox paginate)
        {
            var result = await _dayBoxService.getDayboxs(paginate);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DayboxDto data)
        {
            var result = await _dayBoxService.createDaybox(data);
            return Ok(result);
        }

        [HttpGet("by-date")]
        public async Task<IActionResult> FindByDate([FromQuery] QueryDayBoxByDate query)
        {
            var result = await _dayBoxService.getDayboxByDate(query);
            return Ok(result);
        }

        [HttpGet("is-create-sales/{boxId}")]
        public async Task<IActionResult> IsCreateSales(int boxId)
        {
            var result = await _dayBoxService.isCreateSales(boxId);
            return Ok(result);
        }
    }
}
