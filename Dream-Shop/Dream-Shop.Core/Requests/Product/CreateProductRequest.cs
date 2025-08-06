using Dream_Shop.Database.Models;

namespace Dream_Shop.Core.Requests.Product;

public class CreateProductRequest
{
    public Guid id;
    public string Name;
    public string Brand;
    public string Description;
    public decimal Price;
    public int Inventory;
    public Database.Models.Category Category;
}