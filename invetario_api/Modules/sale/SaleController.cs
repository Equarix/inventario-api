using invetario_api.Modules.sale.dto;
using invetario_api.Modules.sale.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.sale
{

    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var result = await _saleService.getSales();
            return Ok(result);
        }

        [HttpGet("{saleId:int}")]
        public async Task<IActionResult> FindById(int saleId)
        {
            var result = await _saleService.getSaleById(saleId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaleDto data)
        {
            var result = await _saleService.createSale(data);
            return Ok(result);
        }

        [HttpDelete("{saleId:int}")]
        public async Task<IActionResult> delete(int saleId)
        {
            var result = await _saleService.deleteSale(saleId);
            return Ok(result);
        }
    }
}
