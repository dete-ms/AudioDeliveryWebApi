namespace AudioDelivery.Application.Images.DTOs;

public class ImageDto
{
    public ImageDto()
    {
        
    }

    public ImageDto(string url, int width, int height)
    {
        this.Url = url;
        this.Width = width;
        this.Height = height;
    }

    public string Url { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
}
