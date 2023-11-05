using ForumApi.Data.Models;
using ForumApi.DTO.DSearch;
using ForumApi.DTO.Page;

namespace ForumApi.Services.Interfaces
{
    public interface ISearchService
    {
        Task<SearchResponse> SearchTopics(string query, SearchParams search, Page page);
    }
}