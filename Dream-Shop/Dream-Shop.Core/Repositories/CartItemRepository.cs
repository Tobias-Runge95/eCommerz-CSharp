using Dream_Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Dream_Shop.Core.Repositories;

public interface ICartItemRepository : IBaseRepository<CartItem>
{
    Task DeleteAllByCartId(Guid cartId);
}

public class CartItemRepository :  BaseRepository<CartItem>, ICartItemRepository
{
    public async Task DeleteAllByCartId(Guid cartId)
    {
        var cartItems = await _db.CartItems
            .Where(c => c.CartId == cartId).ToListAsync();
        RemoveMany(cartItems);
    }
}