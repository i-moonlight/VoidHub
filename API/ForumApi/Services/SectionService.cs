using AutoMapper;
using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.Auth;
using ForumApi.DTO.DForum;
using ForumApi.DTO.DSection;
using ForumApi.DTO.DTopic;
using ForumApi.Exceptions;
using ForumApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Services
{
    public class SectionService : ISectionService
    {

        private readonly IRepositoryManager _rep;
        private readonly IMapper _mapper;

        public SectionService(
            IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _rep = repositoryManager;
            _mapper = mapper;
        }
        public async Task<List<SectionResponse>> GetSections()
        {
            var sections = await _rep.Section.Value
                .FindByCondition(s => s.IsHidden == false)
                .OrderBy(s => s.OrderPosition)
                .Select(s => new SectionResponse {
                    Id = s.Id,
                    Title = s.Title,
                    IsHidden = s.IsHidden,
                    Forums = s.Forums
                        .Where(f => f.DeletedAt == null)
                        .Select(f => new {
                            Forum = f,
                            Topics = f.Topics.Where(t => t.DeletedAt == null)
                        })
                        .Select(ff => new ForumResponse
                        {
                            Id = ff.Forum.Id,
                            Title = ff.Forum.Title,
                            TopicsCount = ff.Topics.Count(),
                            PostsCount = ff.Topics.SelectMany(t => t.Posts).Where(p=> p.DeletedAt == null).Count(),
                            LastTopic = ff.Topics
                                .OrderByDescending(t => t.Posts.Where(p => p.DeletedAt == null).Max(p => p.CreatedAt))
                                .Select(t => new {
                                    Topic = t,
                                    Posts = t.Posts.Where(p => p.DeletedAt == null)
                                        .OrderByDescending(p => p.CreatedAt)
                                        .Select(p => p)
                                })
                                .Select(t => new TopicLast
                                {
                                    Id = t.Topic.Id,
                                    Title = t.Topic.Title,
                                    UpdatedAt = t.Posts
                                        .First().CreatedAt,
                                    User = _mapper.Map<User>(t.Posts
                                        .Select(p => p.Author)
                                        .FirstOrDefault())
                                }).FirstOrDefault()
                        }).ToList()
                }).ToListAsync();

            return sections;
        }

        public async Task<Section> Create(SectionDto sectionDto)
        {
            var newSection = _rep.Section.Value.Create(_mapper.Map<Section>(sectionDto));

            await _rep.Save();

            return newSection;
        }

        public async Task<Section> Update(int sectionId, SectionDto section)
        {
            var entity = await _rep.Section.Value
                .FindByCondition(s => s.Id == sectionId, true)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Section not found");

            _mapper.Map(section, entity);
            await _rep.Save();

            return entity;
        }
    }
}