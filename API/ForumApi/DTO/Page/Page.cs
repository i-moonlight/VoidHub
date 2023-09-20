using System.ComponentModel.DataAnnotations;

namespace ForumApi.DTO.Page
{
    public class Page
    {
        public int PageNumber { get; set; }
        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
        public int PageSize { get; set; }
    }
}