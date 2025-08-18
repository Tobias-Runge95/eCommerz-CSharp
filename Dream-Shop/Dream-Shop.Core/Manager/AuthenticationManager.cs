using System.Security.Claims;
using Dream_Shop.Core.Requests.Auth;
using Dream_Shop.Core.Response.Auth;
using Dream_Shop.Database;
using Dream_Shop.Database.Models;
using Microsoft.EntityFrameworkCore;
using Roll20Helper.Core.Auth;

namespace Dream_Shop.Core.Manager;

public interface IAuthManager
{
    Task<LoginResponse> Login(LoginRequest loginRequest);
    Task Logout(Guid userId);
    // Task<LoginResponse> RenewToken(RenewRequest renewRequest);
}

public class AuthManager : IAuthManager
{
    private readonly AppDbContext  _context;
    private readonly UserManager _userManager;
    private readonly TokenService _tokenService;

    public AuthManager(AppDbContext context, UserManager userManager, TokenService tokenService)
    {
        _context = context;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> Login(LoginRequest loginRequest)
    {
        var user =  await _userManager.FindByEmailAsync(loginRequest.Email);
        if (user is null)
        {
            throw new Exception("User not found");
        }
        
        bool isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
        if (!isPasswordValid)
        {
            throw new Exception("Invalid password");
        }

        var claims = await GetClaims(user);
        var token = await _tokenService.GenerateTokensAsync(claims);
        await _context.UserTokens.AddAsync(new UserToken()
            { LoginProvider = "API", Name = "Renew", UserId = user.Id, Value = token.RenewToken });
        await _context.SaveChangesAsync();
        token.UserId = user.Id;
        return token;
    }

    public async Task Logout(Guid userId)
    {
        var userToken = await _context.UserTokens.Where(x => x.UserId == userId).FirstAsync();
        _context.UserTokens.Remove(userToken);
        await _context.SaveChangesAsync();
    }

    // public async Task<LoginResponse> RenewToken(RenewRequest renewRequest)
    // {
    //     var user = await _userManager.GetUserAsync(renewRequest.UserId);
    //     var userToken = await _context.UserTokens.Where(x => x.UserId == user.Id).FirstAsync();
    //     if (userToken is null)
    //     {
    //         throw new Exception("No Token found");
    //     }
    //     var claims = await GetClaims(user);
    //     var token = await _tokenService.GenerateTokensAsync(claims);
    //     await _context.UserTokens.AddAsync(new UserToken()
    //         { LoginProvider = "API", Name = "Renew", UserId = user.Id, Value = token.RenewToken });
    //     await _context.SaveChangesAsync();
    //     token.UserId = user.Id;
    //     return token;
    // }

    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        claims.Add(new Claim(ClaimTypes.Role, "Logedin"));
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        return claims.ToList();
    }
}