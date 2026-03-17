using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace invetario_api.Modules.home
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet("trend")]
        public async Task<IActionResult> getTrend()
        {
            var result = await _homeService.getTrend();
            return Ok(result);
        }

        [HttpGet("products-top")]
        public async Task<IActionResult> getProductsTop()
        {
            var result = await _homeService.getProductsTop();
            return Ok(result);
        }

        [HttpGet("kpi")]
        public async Task<IActionResult> getKpi()
        {
            var result = await _homeService.getKpi();
            return Ok(result);
        }

        [HttpGet("critical-products")]
        public async Task<IActionResult> getCriticalProducts()
        {
            var result = await _homeService.getCriticalProducts();
            return Ok(result);
        }

        [HttpGet("categories-top")]
        public async Task<IActionResult> getCategoriesTop()
        {
            var result = await _homeService.getCategoriesTop();
            return Ok(result);
        }
    }
}
