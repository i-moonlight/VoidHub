using ForumApi.Data.Models;
using ForumApi.DTO.DSearch;
using ForumApi.DTO.Page;

namespace ForumApi.Services.Interfaces
{
    public interface ISearchService
    {
        Task<List<Topic>> SearchTopics(string query, SearchParams search, Page page);
    }
}