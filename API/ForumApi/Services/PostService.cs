using AutoMapper;
using ForumApi.Data.Models;
using ForumApi.Data.Repository.Extensions;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.DPost;
using ForumApi.DTO.Page;
using ForumApi.Exceptions;
using ForumApi.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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

        public async Task Delete(int postId)
        {
            var entity = await _rep.Post
                .FindByCondition(p => p.Id == postId && p.DeletedAt == null, true)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Post not found");

            _rep.Post.Delete(entity);
            await _rep.Save();
        }

        public async Task<List<PostResponse>> GetPostPage(int topicId, Page page)
        {
            //skip(1) for not including main post
            var posts = await _rep.Post
                .FindByCondition(p => p.TopicId == topicId && p.DeletedAt == null)
                .OrderBy(p => p.CreatedAt)
                .Include(p => p.Author)
                .Skip(1)
                .TakePage(page)
                .ToListAsync();

            return _mapper.Map<List<PostResponse>>(posts);
        }

        public async Task<Post> Update(int postId, PostDto postDto)
        {
            var entity = await _rep.Post
                .FindByCondition(p => p.Id == postId && p.DeletedAt == null, true)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new Exception("Post not found");

            entity.Content = postDto.Content;

            await _rep.Save();

            return entity;
        }
    }
}