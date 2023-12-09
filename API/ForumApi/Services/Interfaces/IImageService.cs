using SixLabors.ImageSharp;

namespace ForumApi.Services.Interfaces
{
    public interface IImageService
    {
        Image PrepareImage(IFormFile file);
        Image PrepareImage(Image image);
        Task SaveImage(Image image, string path);
    }
}