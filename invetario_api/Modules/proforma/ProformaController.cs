using invetario_api.Modules.proforma.dto;
using invetario_api.Modules.proforma.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using invetario_api.Utils;
using invetario_api.Modules.users.entity;
using Microsoft.AspNetCore.Authorization;


namespace invetario_api.Modules.proforma
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProformaController : ControllerBase
    {
        private IProformaService _proformaService;

        public ProformaController(IProformaService proformaService)
        {
            _proformaService = proformaService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll([FromQuery] QueryDto paginate)
        {
            var result = await _proformaService.getProformas(paginate);
            return Ok(result);
        }

        [HttpGet("{proformaId:int}")]
        public async Task<IActionResult> FindById(int proformaId)
        {
            var result = await _proformaService.getProformaById(proformaId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProformaDto data)
        {
            var result = await _proformaService.createProforma(data);
            return Ok(result);
        }

    }
}
