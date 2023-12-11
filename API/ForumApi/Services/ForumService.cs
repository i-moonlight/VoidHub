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
            return await _rep.Forum.Value
                .FindByCondition(f => f.Id == forumId && f.DeletedAt == null)
                .Select(f => new ForumResponse
                {
                    Id = f.Id,
                    Title = f.Title,
                    SectionId = f.SectionId,
                    IsClosed = f.IsClosed,
                    PostsCount = f.Topics.Where(t => t.DeletedAt == null).SelectMany(t => t.Posts).Where(p => p.DeletedAt == null).Count(),
                    TopicsCount = f.Topics.Where(t => t.DeletedAt == null).Count()
                }).FirstOrDefaultAsync();
        }

        public async Task<Forum> Create(ForumDto forumDto)
        {
            var forum = _rep.Forum.Value.Create(_mapper.Map<Forum>(forumDto));
            await _rep.Save();
            
            return forum;
        }

        public async Task<Forum> Update(int forumId, ForumDto forumDto)
        {
            var entity = await _rep.Forum.Value
                .FindByCondition(f => f.Id == forumId && f.DeletedAt == null, true)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Forum not found");

            _mapper.Map(forumDto, entity);
            await _rep.Save();

            return entity;
        }

        public async Task Delete(int forumId)
        {
            var entity = await _rep.Forum.Value
                .FindByCondition(f => f.DeletedAt == null && f.Id == forumId, true)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Forum not found");

            _rep.Forum.Value.Delete(entity);
            await _rep.Save();
        }
    }
}