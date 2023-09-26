using System.ComponentModel.DataAnnotations;

namespace ForumApi.DTO.Page
{
    public class Page
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}