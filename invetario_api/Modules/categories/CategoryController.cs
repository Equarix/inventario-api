
using invetario_api.Modules.categories.dto;
using invetario_api.Modules.categories.entity;
using invetario_api.Modules.users.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.categories
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FindAll() {

            var result = await _categoryService.getCategories();
            return Ok(result);
        }

        [HttpGet("{categoryId:int}")]
        [Authorize]
        public async Task<IActionResult> FindById(int categoryId) {
            var result = await _categoryService.getCategoryById(categoryId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN,STORE")]
        public async Task<IActionResult> Create([FromBody] CategoryDto data)
        {
            var result = await _categoryService.createCategory(data);
            return Ok(result);
        }

        [HttpPut("{categoryId:int}")]
        [Authorize(Roles = "ADMIN,STORE")]
        public async Task<IActionResult> update(int categoryId, [FromBody] UpdateCategoryDto data)
        {
            var result = await _categoryService.updateCategory(categoryId, data);
            return Ok(result);
        }


        [HttpDelete("{categoryId:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> delete(int categoryId)
        {
            var result = await _categoryService.deleteCategory(categoryId);
            return Ok(result);
        }
    }
}
