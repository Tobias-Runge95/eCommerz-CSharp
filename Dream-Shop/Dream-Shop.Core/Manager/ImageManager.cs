using Dream_Shop.Core.Exceptions;
using Dream_Shop.Core.Repositories;
using Dream_Shop.Core.Requests.Image;
using Dream_Shop.Core.Response.Image;
using Dream_Shop.Database.Models;

namespace Dream_Shop.Core.Manager;

public interface IImageManager
{
    Task<Image> GetImageById(Guid id);
    Task DeleteImageById(Guid id);
    Task UpdateImageById(Guid id, Image imageUpdate);
    Task<List<ImageDTO>> SaveImages(List<CreateImageRequest> images, Guid productId);
}

public class ImageManager : IImageManager
{
    private readonly IImageRepository _imageRepository;

    public ImageManager(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task<Image> GetImageById(Guid id)
    {
        return await _imageRepository.FindImageById(id) ?? throw new ResourceNotFoundException("Image not found");
    }

    public async Task DeleteImageById(Guid id)
    {
        var image =  await _imageRepository.FindImageById(id) ?? throw new ResourceNotFoundException("Image not found");
        _imageRepository.Remove(image);
        await _imageRepository.SaveChangesAsync();
    }

    public async Task UpdateImageById(Guid id, Image imageUpdate)
    {
        var image = await _imageRepository.FindImageById(id) ?? throw new ResourceNotFoundException("Image not found");
        _imageRepository.Update(imageUpdate);
        
        
        await _imageRepository.SaveChangesAsync();
    }

    public async Task<List<ImageDTO>> SaveImages(List<CreateImageRequest> images, Guid productId)
    {
        List<Image> imagesToSave = new List<Image>();
        foreach (var request in images)
        {
            Guid imageId = Guid.NewGuid();
            Image image = new Image()
            {
                Id = imageId,
                Datei = request.Image,
                Name = request.FileName,
                FileName = request.FileName,
                FileType = request.FileType,
                DownloadUrl = $"/api/images/image/download/{imageId}",
                ProductId = productId
            };
            imagesToSave.Add(image);
        }
        _imageRepository.AddMany(imagesToSave);
        await _imageRepository.SaveChangesAsync();
        return imagesToSave.Select(image => image.ToImageDTO()).ToList();
    }
}
