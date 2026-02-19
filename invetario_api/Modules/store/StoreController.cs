using invetario_api.Modules.store.dto;
using invetario_api.Modules.store.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.store
{

    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FindAll()
        {
            var result = await _storeService.getStores();
            return Ok(result);
        }

        [HttpGet("{storeId:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> FindById(int storeId)
        {
            var result = await _storeService.getStoreById(storeId);

            return Ok(result);
        }

        [HttpGet("{storeId:int}/products")]
        [Authorize(Roles = "ADMIN, STORE")]
        public async Task<IActionResult> GetProductsByStore(int storeId)
        {
            var result = await _storeService.getProductsByStore(storeId);
            return Ok(result);
        }

        [HttpGet("{storeId:int}/products/{productStoreId:int}")]
        [Authorize(Roles = "ADMIN, STORE")]
        public async Task<IActionResult> GetProductById(int storeId, int productStoreId)
        {
            var result = await _storeService.getStoreProductById(storeId, productStoreId);
            return Ok(result);
        }

        [HttpPost("{storeId:int}/products")]
        [Authorize(Roles = "ADMIN, STORE")]
        public async Task<IActionResult> AddProductToStore(int storeId, [FromBody] StoreProductDto data)
        {
            var result = await _storeService.addProductToStore(storeId, data);
            return Ok(result);
        }

        [HttpPut("{storeId:int}/products/{productStoreId:int}")]
        [Authorize(Roles = "ADMIN, STORE")]
        public async Task<IActionResult> UpdateStoreProduct(int storeId, int productStoreId, [FromBody] UpdateStoreProductDto data)
        {
            var result = await _storeService.updateStoreProduct(storeId, productStoreId, data);
            return Ok(result);
        }

        [HttpDelete("{storeId:int}/products/{productStoreId:int}")]
        [Authorize(Roles = "ADMIN, STORE")]
        public async Task<IActionResult> RemoveProductFromStore(int storeId, int productStoreId)
        {
            var result = await _storeService.removeProductFromStore(storeId, productStoreId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] StoreDto data)
        {
            var result = await _storeService.createStore(data);
            return Ok(result);
        }

        [HttpPut("{storeId:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> update(int storeId, [FromBody] UpdateStoreDto data)
        {
            var result = await _storeService.updateStore(storeId, data);
            return Ok(result);
        }


        [HttpDelete("{storeId:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> delete(int storeId)
        {
            var result = await _storeService.deleteStore(storeId);
            return Ok(result);
        }
    }
}
