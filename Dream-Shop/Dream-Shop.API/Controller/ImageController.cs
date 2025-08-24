using Dream_Shop.Core.Manager;
using Dream_Shop.Core.Requests.Image;
using Dream_Shop.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dream_Shop.API.Controller;

[Controller, Route("image")]
public class ImageController : ControllerBase
{
    private readonly IImageManager _imageManager;

    public ImageController(IImageManager imageManager)
    {
        _imageManager = imageManager;
    }


    [HttpPost("upload/{productId}")]
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

    [HttpGet("download/{imageId}")]
    public async Task<IActionResult> DownloadImages([FromRoute] Guid imageId)
    {
        var image = await _imageManager.GetImageById(imageId);
        return File(image.Datei, "image/jpeg");
    }

    [HttpPut("update/{imageId}")]
    public async Task<IActionResult> UpdateImage([FromRoute] Guid imageId, [FromForm] IFormFile file)
    {
        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            await _imageManager.UpdateImageById(imageId, new Image()
            {
                Datei = stream.ToArray(),
                FileName = file.FileName,
                FileType = file.ContentType
            });
        }
        
        return Ok();
    }

    [HttpDelete("delete/{imageId}")]
    public async Task<IActionResult> DeleteImage([FromRoute] Guid imageId)
    {
        await _imageManager.DeleteImageById(imageId);
        return Ok();
    }
}