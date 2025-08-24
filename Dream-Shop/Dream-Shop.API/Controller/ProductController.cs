using Dream_Shop.Core.Manager;
using Dream_Shop.Core.Requests.Product;
using Dream_Shop.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dream_Shop.API.Controller;

[Controller, Route("product")]
public class ProductController : ControllerBase
{
    private readonly IProductManager _productManager;

    public ProductController(IProductManager productManager)
    {
        _productManager = productManager;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productManager.GetAllProducts();
        return Ok(products);
    }

    [HttpGet("get/{productId}")]
    public async Task<IActionResult> GetProduct([FromRoute] Guid productId)
    {
        var product = await _productManager.GetProductById(productId);
        return Ok(product);
    }

    [HttpGet("get-all/{name}")]
    public async Task<IActionResult> GetAllProductsByName([FromRoute] string name)
    {
        var products = await _productManager.GetProductsByName(name);
        return Ok(products);
    }

    [HttpGet("get-all/by-brand")]
    public async Task<IActionResult> GetAllProductsByBrand([FromQuery] string brand)
    {
        var products = await _productManager.GetProductsByBrand(brand);
        return Ok(products);
    }

    [HttpGet("get-all/by-category")]
    public async Task<IActionResult> GetAllProductsByCategory([FromQuery] string category)
    {
        var products = await _productManager.GetProductsByCategory(category);
        return Ok(products);
    }

    [HttpGet("get-all/by/brand-and-name")]
    public async Task<IActionResult> GetAllProductsByBrandAndName([FromQuery] string brand, [FromQuery] string name)
    {
        var products = await _productManager.GetProductsByBrandAndName(brand, name);
        return Ok(products);
    }

    [HttpGet("count/by/brand-and-name")]
    public async Task<IActionResult> CountProductsByBrandAndName([FromQuery] string brand, [FromQuery] string category)
    {
        var count = _productManager.CountProductsByBrandAndName(category, brand);
        return Ok(count);
    }

    [HttpGet("get-all/by/category-and-name")]
    public async Task<IActionResult> GetProductByCategoryAndName([FromQuery] string categoryName,
        [FromQuery] string brandNme)
    {
        var products = await _productManager.GetProductsByCategoryAndBrand(categoryName, brandNme);
        return Ok(products);
    }

    [HttpPost("add")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
    {
        Product product = await _productManager.AddProduct(request);
        return Ok(product);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request)
    {
        await _productManager.UpdateProduct(request);
        return Ok();
    }
    
    [HttpDelete("delete/{productId}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId)
    {
        await _productManager.DeleteProduct(productId);
        return Ok();
    }
}