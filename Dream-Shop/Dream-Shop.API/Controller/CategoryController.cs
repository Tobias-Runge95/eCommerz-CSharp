using Dream_Shop.Core.Manager;
using Dream_Shop.Core.Requests.Category;
using Dream_Shop.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dream_Shop.API.Controller;

[Controller, Route("category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryManager _categoryManager;

    public CategoryController(ICategoryManager categoryManager)
    {
        _categoryManager = categoryManager;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryManager.GetAllCategories();
        return Ok(categories);
    }

    [HttpGet("get/{categoryId}")]
    public async Task<IActionResult> GetCategory([FromRoute] Guid categoryId)
    {
        var category = await _categoryManager.GetCategoryById(categoryId);
        return Ok(category);
    }

    [HttpGet("get/{name}")]
    public async Task<IActionResult> GetCategory([FromRoute] string name)
    {
        var category = await _categoryManager.GetCategoryByName(name);
        return Ok(category);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCategory([FromBody] CreateCategoryRequest request)
    {
        var result = await _categoryManager.AddCategory(request);
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request)
    {
        await _categoryManager.UpdateCategory(request);
        return Ok();
    }

    [HttpDelete("delete/{categoryId}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
    {
        await _categoryManager.DeleteCategory(categoryId);
        return Ok();
    }
}