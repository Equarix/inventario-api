using invetario_api.Modules.departureorder.dto;
using invetario_api.Modules.departureorder.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.departureorder
{

    [ApiController]
    [Route("api/[controller]")]
    public class DepartureorderController : ControllerBase
    {
        private IDepartureorderService _departureorderService;

        public DepartureorderController(IDepartureorderService departureorderService) {
            _departureorderService = departureorderService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll() 
        {
            var result = await _departureorderService.getDepartureorders();
            return Ok(result);
        }
        
        [HttpGet("{departureorderId:int}")]
        public async Task<IActionResult> FindById(int departureorderId) 
        {
            var result = await _departureorderService.getDepartureorderById(departureorderId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartureorderDto data)
        {
            var result = await _departureorderService.createDepartureorder(data);
            return Ok(result);
        }

        [HttpPut("{departureorderId:int}")]
        public async Task<IActionResult> update(int departureorderId, [FromBody] UpdateDepartureorderDto data)
        {            
            var result = await _departureorderService.updateDepartureorder(departureorderId, data);
            return Ok(result);
        }


        [HttpDelete("{departureorderId:int}")]
        public async Task<IActionResult> delete(int departureorderId)
        {
            var result = await _departureorderService.deleteDepartureorder(departureorderId);
            return Ok(result);
        }
    }
}
