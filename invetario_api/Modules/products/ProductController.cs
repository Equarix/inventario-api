using System;
using invetario_api.Modules.products.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace invetario_api.Modules.products;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> FindAll()
    {
        var result = await _productService.getProducts();
        return Ok(result);
    }

    [HttpGet("store-available/{storeId:int}")]
    [Authorize]
    public async Task<IActionResult> FindByStoreId(int storeId)
    {
        var result = await _productService.getProudctsByStoreId(storeId);
        return Ok(result);
    }

    [HttpGet("{productId:int}")]
    [Authorize]
    public async Task<IActionResult> FindById(int productId)
    {
        var result = await _productService.getProductById(productId);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN,STORE")]
    public async Task<IActionResult> Create([FromBody] ProductDto productDto)
    {
        var result = await _productService.createProduct(productDto);
        return Ok(result);
    }

    [HttpPut("{productId:int}")]
    [Authorize(Roles = "ADMIN,STORE")]
    public async Task<IActionResult> Update(int productId, [FromBody] UpdateProductDto productDto)
    {
        var result = await _productService.updateProduct(productId, productDto);
        return Ok(result);
    }

    [HttpDelete("{productId:int}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(int productId)
    {
        var result = await _productService.deleteProduct(productId);
        return Ok(result);
    }
}
