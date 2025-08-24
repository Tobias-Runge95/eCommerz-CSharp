using Dream_Shop.Core.Manager;
using Dream_Shop.Core.Requests.Role;

namespace Dream_Shop.API;

public class DataPusher
{
    private readonly RoleManager _roleManager;

    public DataPusher(RoleManager roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task PushRoles()
    {
        List<string> roles = new List<string>(){"admin", "user"};
        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null)
            {
                await _roleManager.CreateRole(new CreateRoleRequest() { Name = roleName }, CancellationToken.None);
            }
        }
    }
}