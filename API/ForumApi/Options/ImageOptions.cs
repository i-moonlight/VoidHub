using System.ComponentModel.DataAnnotations;

namespace ForumApi.Options
{
    public class ImageOptions
    {
        public const string Image = "Image";

        [Required]
        public string Folder { get;set; } = "";
        [Required]
        public string AvatarFolder { get;set; } = "";
        [Required]
        public string AvatarDefault { get;set; } = "";
        [Required]
        public int ResizeWidth { get;set; } = 0;
        [Required]
        public int ResizeHeight { get;set; } = 0;
    }
}