using Dream_Shop.Core.Response.Image;
using Dream_Shop.Core.Response.Order;
using Dream_Shop.Core.Response.OrderItem;
using Dream_Shop.Core.Response.Product;
using Dream_Shop.Database.Models;

namespace Dream_Shop.Core;

public static class DomainDTOMapper
{
    public static ProductDTO ToProductDTO(this Product product)
    {
        return new ProductDTO()
        {
            Brand = product.Brand,
            Category = product.Category,
            Description = product.Description,
            Name = product.Name,
            Price = product.Price,
            Inventory = product.Inventory,
            Id = product.id,
            Images = product.Images.Select(ToImageDTO).ToList()
        };
    }

    public static ImageDTO ToImageDTO(this Image image)
    {
        return new ImageDTO()
        {
            DownloadUrl = image.DownloadUrl,
            FileName = image.FileName,
            Id = image.Id,
        };
    }

    public static OrderDTO ToOrderDTO(this Order order)
    {
        return new OrderDTO()
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems.Select(ToOrderItemDTO).ToList(),
        };
    }

    public static OrderItemDTO ToOrderItemDTO(this OrderItem orderItem)
    {
        return new OrderItemDTO()
        {
            Id = orderItem.Id,
            Quantity = orderItem.Quantity,
            Price = orderItem.Price,
        };
    }
}