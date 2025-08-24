namespace Dream_Shop.Database.Models;

public class Product
{
    public Guid id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public Category Category { get; set; }
    public Guid CategoryId { get; set; }
    public List<Image> Images { get; set; }
}