namespace Dream_Shop.Core.Requests.Auth;

public class RenewRequest
{
    public Guid UserId { get; set; }
    public string RenewToken { get; set; }
}