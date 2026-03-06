using invetario_api.Modules.client.dto;
using invetario_api.Modules.client.entity;
using invetario_api.utils;
using invetario_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.client
{

    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FindAll([FromQuery] PaginateDto paginate)
        {
            var result = await _clientService.getClients(paginate);
            return Ok(result);
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] string documentNumber)
        {
            var result = await _clientService.searchClients(documentNumber);
            return Ok(result);
        }


        [HttpGet("{clientId:int}")]
        [Authorize]
        public async Task<IActionResult> FindById(int clientId)
        {
            var result = await _clientService.getClientById(clientId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ClientDto data)
        {
            var result = await _clientService.createClient(data);
            return Ok(result);
        }

        [HttpPut("{clientId:int}")]
        [Authorize]
        public async Task<IActionResult> update(int clientId, [FromBody] UpdateClientDto data)
        {
            var result = await _clientService.updateClient(clientId, data);
            return Ok(result);
        }


        [HttpDelete("{clientId:int}")]
        [Authorize]
        public async Task<IActionResult> delete(int clientId)
        {
            var result = await _clientService.deleteClient(clientId);
            return Ok(result);
        }
    }
}
