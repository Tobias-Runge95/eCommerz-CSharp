namespace Dream_Shop.Database.Models;

public class Category
{
    public Category(string name)
    {
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; }
    
    
}