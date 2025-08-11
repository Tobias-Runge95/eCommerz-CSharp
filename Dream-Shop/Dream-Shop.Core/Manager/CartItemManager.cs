using Dream_Shop.Core.Exceptions;
using Dream_Shop.Core.Repositories;
using Dream_Shop.Core.Requests.CartItem;
using Dream_Shop.Database.Models;

namespace Dream_Shop.Core.Manager;

public interface ICartItemManager
{
    Task AddItemToCart(Guid cartId, Guid productId, int quantity);
    Task RemoveItemFromCart(Guid cartId, Guid productId);
    Task UpdateCartItem(UpdateCartItemRequest request);
    Task<CartItem> GetCartItem(Guid cartId, Guid productId);
}

public class CartItemManager : ICartItemManager
{
    
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICartManager _cartManager;
    private readonly ICartRepository _cartRepository;

    public CartItemManager(ICartItemRepository cartItemRepository, IProductRepository productRepository, ICartRepository cartRepository, ICartManager cartManager)
    {
        _cartItemRepository = cartItemRepository;
        _productRepository = productRepository;
        _cartRepository = cartRepository;
        _cartManager = cartManager;
    }

    public async Task AddItemToCart(Guid cartId, Guid productId, int quantity)
    {
        try
        {
            var cart = await _cartManager.GetCartById(cartId);
            _cartRepository.Update(cart);
            if (cart.CartItems.Any(i => i.ProductId == productId))
            {
                var cartItem = cart.CartItems.First(ci => ci.ProductId == productId);
                _cartItemRepository.Update(cartItem);
                cartItem.Quantity = quantity;
            }
            else
            {
                var product = await _productRepository.findById(productId);
                var cartItem = new CartItem(quantity, product.Price, cartId, productId);
                cartItem.SetTotalPrice();
                cart.CartItems.Add(cartItem);
                _cartItemRepository.Add(cartItem);
            }
            await _cartRepository.SaveChangesAsync();
        }
        catch (ResourceNotFoundException e)
        {
            
        }

    }

    public async Task RemoveItemFromCart(Guid cartId, Guid productId)
    {
        var cart = await _cartManager.GetCartById(cartId);
        var cartItem = cart.CartItems.First(i => i.ProductId == productId) ?? throw new ResourceNotFoundException("CartItem not found");
        _cartRepository.Update(cart);
        cart.CartItems.Remove(cartItem);
        _cartItemRepository.Remove(cartItem);
        await _cartRepository.SaveChangesAsync();
    }

    public async Task UpdateCartItem(UpdateCartItemRequest request)
    {
        var cart = await _cartManager.GetCartById(request.CartId);
        var cartItem = cart.CartItems.First(i => i.ProductId == request.ProductId);
        _cartRepository.Update(cart);
        _cartItemRepository.Update(cartItem);
        cartItem.Quantity = request.Quantity;
        await _cartRepository.SaveChangesAsync();
    }

    public async Task<CartItem> GetCartItem(Guid cartId, Guid productId)
    {
        var cart = await _cartManager.GetCartById(cartId);
        return cart.CartItems.FirstOrDefault(i => i.ProductId == productId) ??  throw new ResourceNotFoundException("Cart not found");
    }
}