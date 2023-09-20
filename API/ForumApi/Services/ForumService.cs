using AutoMapper;
using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.DForum;
using ForumApi.Services.Interfaces;

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

        public async Task<Forum> Create(ForumDto forumDto)
        {
            var forum = _rep.Forum.Create(_mapper.Map<Forum>(forumDto));
            await _rep.Save();
            
            return forum;
        }
    }
}