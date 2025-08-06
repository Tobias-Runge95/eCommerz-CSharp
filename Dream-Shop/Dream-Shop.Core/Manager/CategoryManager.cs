using Dream_Shop.Core.Exceptions;
using Dream_Shop.Core.Repositories;
using Dream_Shop.Core.Requests.Category;
using Dream_Shop.Database.Models;

namespace Dream_Shop.Core.Manager;

public interface ICategoryManager
{
    Task<Category> AddCategory(CreateCategoryRequest request);
    Task<Category> UpdateCategory(UpdateCategoryRequest request);
    Task DeleteCategory(Guid id);
    Task<Category> GetCategoryById(Guid id);
    Task<Category> GetCategoryByName(string name);
    Task<List<Category>> GetAllCategories();
}

public class CategoryManager : ICategoryManager
{
    private readonly ICategoriesRepository  _categoriesRepository;

    public CategoryManager(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<Category> AddCategory(CreateCategoryRequest request)
    {
        if (await _categoriesRepository.ExistsByName(request.Name))
        {
            throw new Exception($"Category with name {request.Name} already exists!");
        }

        var category = new Category(request.Name);
        _categoriesRepository.Add(category);
        await _categoriesRepository.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateCategory(UpdateCategoryRequest request)
    {
        var category = await _categoriesRepository.FindByName(request.Name);
        if (category is null)
        {
            throw new ResourceNotFoundException("Category with name " + request.Name);
        }
        _categoriesRepository.Update(category);
        category.Name = request.Name;
        await _categoriesRepository.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategory(Guid id)
    {
        var category = await _categoriesRepository.FindById(id);
        if (category is null)
        {
            throw new ResourceNotFoundException("Category with " + id);
        }
        _categoriesRepository.Remove(category);
        await _categoriesRepository.SaveChangesAsync();
    }

    public async Task<Category> GetCategoryById(Guid id)
    {
        return await _categoriesRepository.FindById(id) ??  throw new ResourceNotFoundException("Category with " + id);
    }

    public async Task<Category> GetCategoryByName(string name)
    {
        return await _categoriesRepository.FindByName(name) ??  throw new ResourceNotFoundException("Category with " + name);
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await _categoriesRepository.GetAllCategories();
    }
}