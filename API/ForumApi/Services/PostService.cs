using AutoMapper;
using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.DPost;
using ForumApi.Services.Interfaces;

namespace ForumApi.Services
{
    public class PostService : IPostService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;

        public PostService(
            IRepositoryManager rep,
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public async Task<Post> Create(int accountId, PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            post.AccountId = accountId;

            _rep.Post.Create(post);
            await _rep.Save();

            return post;
        }
    }
}