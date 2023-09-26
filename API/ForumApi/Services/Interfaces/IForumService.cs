using ForumApi.Data.Models;
using ForumApi.DTO.DForum;

namespace ForumApi.Services.Interfaces
{
    public interface IForumService
    {
        Task<Forum> Create(ForumDto forumDto);
        Task<ForumResponse?> Get(int forumId);
    }
}