using ForumApi.Data.Models;
using ForumApi.DTO.DPost;
using ForumApi.DTO.Page;

namespace ForumApi.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<PostResponse>> GetPostPage(int topicId, Page page);
        Task<List<PostResponse>> GetPostComments(int? ancestorId, Offset page);
        Task<Post> Create(int accountId, PostDto postDto);
        Task<Post> Update(int postId , PostDto postDto);
        Task Delete(int postId);
    }
}