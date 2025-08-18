using Dream_Shop.Core.Manager;
using Dream_Shop.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Roll20Helper.Core.Auth;

namespace Dream_Shop.Core;

public static class Startup
{
    public static IServiceCollection RegisterServices(this IServiceCollection service)
    {
        return service
            .AddScoped<TokenService>()
            .RegisterManagers()
            .RegisterRepositories();
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection service)
    {
        return service
            .AddScoped<IUserRepository,UserRepository>()
            .AddScoped<ICartItemRepository,CartItemRepository>()
            .AddScoped<ICartRepository,CartRepository>()
            .AddScoped<ICategoriesRepository,CategoryRepository>()
            .AddScoped<IImageRepository,ImageRepository>()
            .AddScoped<IOrderRepository,OrderRepository>()
            .AddScoped<IOrderItemRepository,OrderItemRepository>()
            .AddScoped<IProductRepository,ProductRepository>();
    }

    private static IServiceCollection RegisterManagers(this IServiceCollection service)
    {
        return service
            .AddScoped<IAuthManager, AuthManager>()
            .AddScoped<UserManager>()
            .AddScoped<ICartManager, CartManager>()
            .AddScoped<IOrderManager, OrderManager>()
            .AddScoped<IOrderItemManager, OrderItemManager>()
            .AddScoped<IProductManager, ProductManager>()
            .AddScoped<ICartItemManager, CartItemManager>()
            .AddScoped<IRoleManager, RoleManager>()
            .AddScoped<ICategoryManager, CategoryManager>()
            .AddScoped<IImageManager, ImageManager>();
    }
}