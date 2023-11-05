using ForumApi.Data.Models;

namespace ForumApi.DTO.DSearch
{
    public class SearchResponse
    {
        public int SearchCount { get; set; }
        public List<Topic> Topics { get; set; } = new();
    }
}