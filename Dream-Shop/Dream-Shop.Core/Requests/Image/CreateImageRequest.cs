namespace Dream_Shop.Core.Requests.Image;

public class CreateImageRequest
{
    public byte[] Image { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
}