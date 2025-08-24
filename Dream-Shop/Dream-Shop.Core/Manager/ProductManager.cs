using Dream_Shop.Core.Exceptions;
using Dream_Shop.Core.Repositories;
using Dream_Shop.Core.Requests.Product;
using Dream_Shop.Core.Response.Product;
using Dream_Shop.Database.Models;

namespace Dream_Shop.Core.Manager;

public interface IProductManager
{
    Task<Product> AddProduct(CreateProductRequest request);
    Task<Product> UpdateProduct(UpdateProductRequest request);
    Task DeleteProduct(Guid productId);
    Task<List<ProductDTO>> GetAllProducts();
    Task<ProductDTO> GetProductById(Guid id);
    Task<List<ProductDTO>> GetProductsByName(string name);
    Task<List<ProductDTO>> GetProductsByCategory(string category);
    Task<List<ProductDTO>> GetProductsByBrand(string brand);
    Task<List<ProductDTO>> GetProductsByBrandAndName(string brand, string name);
    Task<List<ProductDTO>> GetProductsByCategoryAndBrand(string category, string brand);
    long CountProductsByBrandAndName(string brand, string name);
}

public class ProductManager : IProductManager
{
    
    private readonly IProductRepository _productRepository;
    private readonly ICategoriesRepository _categoriesRepository;

    public ProductManager(IProductRepository productRepository, ICategoriesRepository categoriesRepository)
    {
        _productRepository = productRepository;
        _categoriesRepository = categoriesRepository;
    }


    public async Task<Product> AddProduct(CreateProductRequest request)
    {
        if (await ProductExists(request.Name, request.Brand))
        {
            throw new Exception();
        }

        var category = await _categoriesRepository.FindByName(request.Category);
        if (category is null)
        {
            category = new Category(request.Category);
            _categoriesRepository.Add(category);
        }

        var product = new Product()
        {
            Category = category,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Brand = request.Brand
        };
        _productRepository.Add(product);
        await _productRepository.SaveChangesAsync();
        return product;
    }

    private async Task<bool> ProductExists(string name, string brand)
    {
        return await _productRepository.existsByNameAndBrand(name, brand);
    }

    public async Task<Product> UpdateProduct(UpdateProductRequest request)
    {
        var product = await _productRepository.findById(request.id);
        if (product is null)
        {
            throw new ResourceNotFoundException("Product not found");
        }
        
        _productRepository.Update(product);
        
        foreach (var prop in typeof(UpdateProductRequest).GetProperties())
        {
            var targetProp = typeof(User).GetProperty(prop.Name);
            if (targetProp != null && targetProp.CanWrite)
            {
                targetProp.SetValue(product, prop.GetValue(request));
            }
        }

        await _productRepository.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProduct(Guid productId)
    {
        var product = await _productRepository.findById(productId);
        if (product is null)
        {
            throw new ResourceNotFoundException("Product not found");
        }
        _productRepository.Remove(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task<List<ProductDTO>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProducts();
        if (products.Count == 0)
        {
            throw new ResourceNotFoundException("Products not found");
        }
        var productDTOs = products.Select(x => x.ToProductDTO()).ToList();
        return productDTOs;
    }

    public async Task<ProductDTO> GetProductById(Guid id)
    {
        var product =  await _productRepository.findById(id);
        if (product is null)
        {
            throw new ResourceNotFoundException("Product not found");
        }
        
        return product.ToProductDTO();
    }

    public async Task<List<ProductDTO>> GetProductsByName(string name)
    {
        var products = await _productRepository.findByName(name);
        if (products.Count == 0)
        {
            throw new ResourceNotFoundException("Products not found");
        }
        
        return products.Select(x => x.ToProductDTO()).ToList();
    }

    public async Task<List<ProductDTO>> GetProductsByCategory(string category)
    {
        var products = await _productRepository.findByCategoryName(category);
        if (products.Count == 0)
        {
            throw new ResourceNotFoundException("Products not found");
        }
        
        return products.Select(x => x.ToProductDTO()).ToList();
    }

    public async Task<List<ProductDTO>> GetProductsByBrand(string brand)
    {
        var products = await _productRepository.findByBrand(brand);
        if (products.Count == 0)
        {
            throw new ResourceNotFoundException("Products not found");
        }
        
        return products.Select(x => x.ToProductDTO()).ToList();
    }

    public async Task<List<ProductDTO>> GetProductsByBrandAndName(string brand, string name)
    {
        var products = await _productRepository.findByBrandAndName(brand, name);
        if (products.Count == 0)
        {
            throw new ResourceNotFoundException("Products not found");
        }
        
        return products.Select(x => x.ToProductDTO()).ToList();
    }

    public async Task<List<ProductDTO>> GetProductsByCategoryAndBrand(string category, string brand)
    {
        var products = await _productRepository.findByCategoryNameAndBrand(category, brand);
        return products.Select(x => x.ToProductDTO()).ToList();
    }

    public long CountProductsByBrandAndName(string brand, string name)
    {
        return _productRepository.countByBrandAndName(brand, name);
    }
}