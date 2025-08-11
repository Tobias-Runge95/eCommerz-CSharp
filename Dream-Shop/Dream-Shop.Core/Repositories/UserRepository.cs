using Dream_Shop.Database;
using Dream_Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Dream_Shop.Core.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetUserAsync(Guid userId);
    Task<User> GetUserAsync(string userName);
    Task<User> GetUserByEmailAsync(string email);
}

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext db) : base(db)
    {
    }

    public Task<User> GetUserAsync(Guid userId)
    {
        return _db.Users
            .Where(u => u.Id == userId)
            .Include(u => u.Orders)
            .Include(u => u.Cart)
            .FirstOrDefaultAsync()!;
    }

    public Task<User> GetUserAsync(string userName)
    {
        return _db.Users.FirstOrDefaultAsync(u => u.UserName == userName)!;
    }

    public Task<User> GetUserByEmailAsync(string email)
    {
        return _db.Users.FirstOrDefaultAsync(u => u.Email == email)!;
    }
}