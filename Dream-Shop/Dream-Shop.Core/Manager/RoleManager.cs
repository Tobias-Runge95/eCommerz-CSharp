using Dream_Shop.Database;
using Dream_Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Dream_Shop.Core.Manager;

public interface IRoleManager
{
    // Task CreateRole(CreateRole request, CancellationToken cancellationToken);
    // Task<List<Role>> GetAllRoles(CancellationToken cancellationToken);
    // Task DeleteRole(DeleteRole request, CancellationToken cancellationToken);
    // Task UpdateRole(UpdateRole request, CancellationToken cancellationToken);
    Task<Role?> FindByNameAsync(string name, CancellationToken cancellationToken);
}

public class RoleManager : IRoleManager
{
    private readonly AppDbContext _dbContext;

    public RoleManager(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // public Task CreateRole(CreateRole request, CancellationToken cancellationToken)
    // {
    //     throw new NotImplementedException();
    // }
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