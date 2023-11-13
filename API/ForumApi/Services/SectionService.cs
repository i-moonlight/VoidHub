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
                        .Select(ff => new ForumResponse
                        {
                            Id = ff.Id,
                            Title = ff.Title,
                            TopicsCount = ff.Topics.Where(t => t.DeletedAt == null).Count(),
                            PostsCount = ff.Topics.Where(t => t.DeletedAt == null)
                                .SelectMany(t => t.Posts).Where(p=> p.DeletedAt == null).Count(),
                            LastTopic = ff.Topics
                                .Where(t => t.DeletedAt == null)
                                .OrderByDescending(t => t.Posts.Where(p => p.DeletedAt == null).Max(p => p.CreatedAt))
                                .Select(t => new TopicLast
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    UpdatedAt = t.Posts
                                        .Where(p => p.DeletedAt == null)
                                        .OrderByDescending(p => p.CreatedAt)
                                        .First().CreatedAt,
                                    User = _mapper.Map<User>(t.Posts
                                        .Where(p => p.DeletedAt == null)
                                        .OrderByDescending(p => p.CreatedAt)
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