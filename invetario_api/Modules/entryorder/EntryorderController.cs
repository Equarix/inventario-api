using invetario_api.Modules.entryorder.dto;
using invetario_api.Modules.entryorder.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.entryorder
{

    [ApiController]
    [Route("api/[controller]")]
    public class EntryorderController : ControllerBase
    {
        private IEntryorderService _entryorderService;

        public EntryorderController(IEntryorderService entryorderService)
        {
            _entryorderService = entryorderService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FindAll()
        {
            var result = await _entryorderService.getEntryorders();
            return Ok(result);
        }

        [HttpGet("{entryorderId:int}")]
        [Authorize]
        public async Task<IActionResult> FindById(int entryorderId)
        {
            var result = await _entryorderService.getEntryorderById(entryorderId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] EntryorderDto data)
        {
            var result = await _entryorderService.createEntryorder(data);
            return Ok(result);
        }

        [HttpPost("{entryorderId:int}/complete")]
        [Authorize]
        public async Task<IActionResult> Complete(int entryorderId)
        {
            var result = await _entryorderService.completeEntryorder(entryorderId);
            return Ok(result);
        }

        [HttpDelete("{entryorderId:int}/cancel")
        ]
        [Authorize]
        public async Task<IActionResult> Cancel(int entryorderId)
        {
            var result = await _entryorderService.cancelEntryorder(entryorderId);
            return Ok(result);
        }
    }
}
