namespace Dream_Shop.Database.Models;

public class Image
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public string DownloadUrl { get; set; }
    public byte[] Datei { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
}