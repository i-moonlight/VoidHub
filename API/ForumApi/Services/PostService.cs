using AutoMapper;
using ForumApi.Data.Models;
using ForumApi.Data.Repository.Extensions;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.Auth;
using ForumApi.DTO.DPost;
using ForumApi.DTO.Page;
using ForumApi.Exceptions;
using ForumApi.Services.Interfaces;
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

            _rep.Post.Value.Create(post);
            await _rep.Save();

            return post;
        }

        public async Task Delete(int postId)
        {
            var entity = await _rep.Post.Value
                .FindByCondition(p => p.Id == postId && p.DeletedAt == null, true)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Post not found");

            var topicFirstPost = await _rep.Post.Value
                .FindByCondition(p => p.TopicId == entity.TopicId && p.DeletedAt == null)
                .OrderBy(p => p.CreatedAt)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Post not found");

            if (entity.Id == topicFirstPost.Id)
                throw new BadRequestException("You can't delete main post");            

            _rep.Post.Value.Delete(entity);
            await _rep.Save();
        }

        public async Task<List<PostResponse>> GetPostComments(int? ancestorId, Offset page)
        {
            var posts = await _rep.Post.Value
                .FindByCondition(p => p.DeletedAt == null && p.AncestorId == ancestorId)
                .OrderByDescending(p => p.CreatedAt)
                .Include(p => p.Author)
                .TakeOffset(page)
                .Select(p => new PostResponse
                {
                    Id = p.Id,
                    TopicId = p.TopicId,
                    Author = _mapper.Map<User>(p.Author),
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    CommentsCount = p.Comments.Where(c => c.DeletedAt == null).Count()
                })
                .ToListAsync();

            return posts;
        }

        public async Task<Post> Update(int postId, PostDto postDto)
        {
            var entity = await _rep.Post.Value
                .FindByCondition(p => p.Id == postId && p.DeletedAt == null, true)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Post not found");

            entity.Content = postDto.Content;
            await _rep.Save();

            return entity;
        }
    }
}