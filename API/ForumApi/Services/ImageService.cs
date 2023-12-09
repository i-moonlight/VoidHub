
using ForumApi.Options;
using ForumApi.Services.Interfaces;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace ForumApi.Services
{
    public class ImageService(IOptions<ImageOptions> imageOptions) : IImageService
    {
        private readonly ImageOptions _imageOptions = imageOptions.Value;

        public Image PrepareImage(IFormFile file)
        {
            var image = Image.Load(file.OpenReadStream());
            return PrepareImage(image);
        }

        public Image PrepareImage(Image image)
        {
            var minSize = Math.Min(image.Width, image.Height);
            image.Mutate(x => x.Crop(new SixLabors.ImageSharp.Rectangle((image.Width - minSize) / 2, (image.Height - minSize) / 2, minSize, minSize)));
            image.Mutate(x => x.Resize(_imageOptions.ResizeWidth, _imageOptions.ResizeHeight));

            return image;
        }

        public async Task SaveImage(Image image, string path)
        {
            var encoder = new PngEncoder();
            await using var stream = new FileStream(path, FileMode.Create);
            await image.SaveAsync(stream, encoder);
        }
    }
}