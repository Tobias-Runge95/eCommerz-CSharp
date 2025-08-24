using Dream_Shop.Core.Requests.Role;
using Dream_Shop.Database;
using Dream_Shop.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dream_Shop.Core.Manager;

public class RoleManager : RoleManager<Role>
{
    private readonly AppDbContext _dbContext;
    
    public RoleManager(IRoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<Role>> logger, AppDbContext dbContext) : base(store, roleValidators, keyNormalizer, errors, logger)
    {
        _dbContext = dbContext;
    }

    public async Task CreateRole(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        var role = new Role() { Id = Guid.NewGuid(), Name = request.Name, NormalizedName = request.Name.ToUpper() };
        await CreateAsync(role);
        // _dbContext.Roles.Add(role);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    //
    // public async Task<List<Role>> GetAllRoles(CancellationToken cancellationToken)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public async Task DeleteRole(DeleteRole request, CancellationToken cancellationToken)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public async Task UpdateRole(UpdateRole request, CancellationToken cancellationToken)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<Role?> FindByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }
}