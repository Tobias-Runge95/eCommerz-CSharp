namespace Dream_Shop.Core.Requests.User;

public class UpdateUser
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}