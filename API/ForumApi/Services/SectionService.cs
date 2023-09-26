using AutoMapper;
using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.Auth;
using ForumApi.DTO.DForum;
using ForumApi.DTO.DSection;
using ForumApi.DTO.DTopic;
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
            var sections = await _rep.Section
                .FindByCondition(s => s.IsHidden == false)
                .OrderBy(s => s.OrderPosition)
                .Include(s => s.Forums.Where(f => f.DeletedAt == null))
                .ThenInclude(f => f.Topics.Where(t => t.DeletedAt == null))
                .ThenInclude(t => t.Posts.Where(p => p.DeletedAt == null))
                .ThenInclude(p => p.Author)
                .Select(s => new SectionResponse {
                    Id = s.Id,
                    Title = s.Title,
                    Forums = s.Forums
                        .Select(ff => new ForumResponse
                        {
                            Id = ff.Id,
                            Title = ff.Title,
                            TopicsCount = ff.Topics.Count(),
                            MsgsCount = ff.Topics.SelectMany(t => t.Posts).Count(),
                            LastTopic = ff.Topics
                                .OrderByDescending(t => t.CreatedAt)
                                .Select(t => new TopicLast
                                {
                                    Id = t.Id,
                                    Title = t.Title,
                                    CreatedAt = t.CreatedAt,
                                    User = _mapper.Map<User>(t.Posts
                                        .OrderByDescending(p => p.CreatedAt)
                                        .Select(p => p.Author)
                                        .FirstOrDefault())
                                }).FirstOrDefault()
                        }).ToList()
                })
                .ToListAsync();

            return sections;
        }

        public async Task<Section> Create(SectionDto sectionDto)
        {
            var newSection = _rep.Section.Create(_mapper.Map<Section>(sectionDto));

            await _rep.Save();

            return newSection;
        }
    }
}