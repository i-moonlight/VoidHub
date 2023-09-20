using ForumApi.Data.Models;
using ForumApi.DTO.DPost;

namespace ForumApi.Services.Interfaces
{
    public interface IPostService
    {
        Task<Post> Create(int accountId, PostDto postDto);
    }
}