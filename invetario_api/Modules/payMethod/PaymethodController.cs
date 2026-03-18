using invetario_api.Modules.payMethod.dto;
using invetario_api.Modules.payMethod.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.payMethod
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymethodController : ControllerBase
    {
        private IPaymethodService _payMethodService;

        public PaymethodController(IPaymethodService payMethodService)
        {
            _payMethodService = payMethodService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var result = await _payMethodService.getPaymethods();
            return Ok(result);
        }

        [HttpGet("active")]
        public async Task<IActionResult> FindAllActive()
        {
            var result = await _payMethodService.getPaymethodsActive();
            return Ok(result);
        }

        [HttpGet("{payMethodId:int}")]
        public async Task<IActionResult> FindById(int payMethodId)
        {
            var result = await _payMethodService.getPaymethodById(payMethodId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymethodDto data)
        {
            var result = await _payMethodService.createPaymethod(data);
            return Ok(result);
        }

        [HttpPut("{payMethodId:int}")]
        public async Task<IActionResult> update(int payMethodId, [FromBody] UpdatePaymethodDto data)
        {
            var result = await _payMethodService.updatePaymethod(payMethodId, data);
            return Ok(result);
        }


        [HttpDelete("{payMethodId:int}")]
        public async Task<IActionResult> delete(int payMethodId)
        {
            var result = await _payMethodService.deletePaymethod(payMethodId);
            return Ok(result);
        }
    }
}
