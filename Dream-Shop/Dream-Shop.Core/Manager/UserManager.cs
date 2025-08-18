using Dream_Shop.Core.Repositories;
using Dream_Shop.Core.Requests.User;
using Dream_Shop.Database;
using Dream_Shop.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dream_Shop.Core.Manager;

public class UserManager : UserManager<User>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleManager _roleManager;
    private readonly AppDbContext _context;

    public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
        IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger,
        IUserRepository userRepository, IRoleManager roleManager, AppDbContext context) : base(store, optionsAccessor,
        passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
        _userRepository = userRepository;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<User> CreateAsync(CreateUser request, CancellationToken cancellationToken)
    {
        var user = new User() { Id = Guid.NewGuid(),UserName = $"{request.FirstName}, {request.LastName}", Email = request.Email };
        var result = await CreateAsync(user, request.Password);
        _userRepository.Add(user);
        var roleResult = await _roleManager.FindByNameAsync("user", cancellationToken);
        if (roleResult is not null)
        {
            _context.UserRoles.Add(new UserRole { RoleId = roleResult.Id, UserId = user.Id });
        }
        
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task DeleteAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(userId);
        _userRepository.Remove(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserAsync(userId);
    }

    public async Task UpdateUserAsync(UpdateUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(request.UserId);
        _userRepository.Update(user);
        user.UserName = $"{request.FirstName}, {request.LastName}";
        user.NormalizedUserName = $"{request.FirstName} {request.LastName}".ToUpper();

        await _userRepository.SaveChangesAsync();
    }
}