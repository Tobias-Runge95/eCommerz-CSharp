using Dream_Shop.Core.Manager;
using Dream_Shop.Core.Requests.Image;
using Microsoft.AspNetCore.Mvc;

namespace Dream_Shop.API.Controller;

[Controller, Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageManager _imageManager;

    public ImageController(IImageManager imageManager)
    {
        _imageManager = imageManager;
    }


    [HttpPost("/upload/{productId}")]
    public async Task<IActionResult> UploadImages([FromForm] List<IFormFile> files, [FromRoute] Guid productId)
    {
        List<CreateImageRequest> requests = new List<CreateImageRequest>();
        foreach (var file in files)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                requests.Add(new CreateImageRequest()
                {
                    FileName = file.FileName,
                    FileType = file.ContentType,
                    Image = stream.ToArray()
                });
            }
            
        }
        await _imageManager.SaveImages(requests, productId);
        return Ok();
    }
}