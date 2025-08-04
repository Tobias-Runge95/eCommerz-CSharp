namespace Dream_Shop.Database.Models;

public class Product
{
    public Guid id;
    public string Name;
    public string Brand;
    public string Description;
    public decimal Price;
    public int Inventory;
    public Category Category;
    public Guid CategoryId { get; set; }
    public List<Image> Images { get; set; }
}