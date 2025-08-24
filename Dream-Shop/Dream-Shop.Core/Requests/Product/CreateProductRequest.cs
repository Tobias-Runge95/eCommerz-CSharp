using Dream_Shop.Database.Models;

namespace Dream_Shop.Core.Requests.Product;

public class CreateProductRequest
{
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public string Category { get; set; }
}