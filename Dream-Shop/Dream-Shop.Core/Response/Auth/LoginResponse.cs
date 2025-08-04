namespace Dream_Shop.Core.Response.Auth;

public class LoginResponse
{
    public string AuthToken { get; set; }
    public string RenewToken { get; set; }
    public Guid UserId { get; set; }
}