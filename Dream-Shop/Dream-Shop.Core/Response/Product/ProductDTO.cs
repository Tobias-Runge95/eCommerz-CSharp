using Dream_Shop.Core.Response.Image;
using Dream_Shop.Database.Models;

namespace Dream_Shop.Core.Response.Product;

public class ProductDTO
{
    public Guid Id;
    public string Name;
    public string Brand;
    public string Description;
    public decimal Price;
    public int Inventory;
    public Category Category;
    public List<ImageDTO> Images { get; set; }
}