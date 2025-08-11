using Dream_Shop.Core.Exceptions;
using Dream_Shop.Core.Repositories;
using Dream_Shop.Database.Models;

namespace Dream_Shop.Core.Manager;

public interface ICartManager
{
    Task<Cart> GetCartByUserId(Guid userId);
    Task<Cart> GetCartById(Guid cartId);
    Task<decimal> GetTotalAmount(Guid cartId);
    Task<Cart> InitializeCart(Guid userId);
    Task ClearCard(Guid cartId);
}

public class CartManager :  ICartManager
{
    
    private readonly ICartRepository _cartRepository;

    public CartManager(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<Cart> GetCartByUserId(Guid userId)
    {
        return await _cartRepository.GetCartByUserId(userId) ?? throw new ResourceNotFoundException("Cart not found");
    }

    public async Task<Cart> GetCartById(Guid cartId)
    {
        return await _cartRepository.GetCartById(cartId) ?? throw new ResourceNotFoundException("Cart not found");
    }

    public async Task<decimal> GetTotalAmount(Guid cartId)
    {
        return await _cartRepository.GetTotalAmount(cartId);
    }

    public async Task<Cart> InitializeCart(Guid userId)
    {
        Cart cart = new Cart();
        cart.UserId = userId;
        _cartRepository.Add(cart);
        await _cartRepository.SaveChangesAsync();
        return cart;
    }

    public async Task ClearCard(Guid cartId)
    {
        var cart = await _cartRepository.GetCartById(cartId);
        _cartRepository.Remove(cart);
        await _cartRepository.SaveChangesAsync();
    }
}