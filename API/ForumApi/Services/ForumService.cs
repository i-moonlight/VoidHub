using AutoMapper;
using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.DForum;
using ForumApi.Exceptions;
using ForumApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Services
{
    public class ForumService : IForumService
    {
        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;

        public ForumService(
            IRepositoryManager rep,
            IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public async Task<ForumResponse?> Get(int forumId)
        {
            return await _rep.Forum
                .FindByCondition(f => f.Id == forumId && f.DeletedAt == null)
                .Select(f => new ForumResponse
                {
                    Id = f.Id,
                    Title = f.Title,
                    PostsCount = f.Topics.SelectMany(t => t.Posts).Where(p => p.DeletedAt == null).Count(),
                    TopicsCount = f.Topics.Where(t => t.DeletedAt == null).Count()
                }).FirstOrDefaultAsync();
        }

        public async Task<Forum> Create(ForumDto forumDto)
        {
            var forum = _rep.Forum.Create(_mapper.Map<Forum>(forumDto));
            await _rep.Save();
            
            return forum;
        }

        public async Task Delete(int forumId)
        {
            var entity = await _rep.Forum
                .FindByCondition(f => f.DeletedAt == null && f.Id == forumId, true)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Forum not found");

            _rep.Forum.Delete(entity);
            await _rep.Save();
        }
    }
}