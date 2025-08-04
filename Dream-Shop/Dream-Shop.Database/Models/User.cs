using Microsoft.AspNetCore.Identity;

namespace Dream_Shop.Database.Models;

public class User : IdentityUser<Guid>
{
    public string RefreshToken { get; set; }
    public Cart Cart { get; set; }
    public Guid CartId { get; set; }
    public List<Order> Orders { get; set; }
}