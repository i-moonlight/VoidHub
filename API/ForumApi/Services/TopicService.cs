using AutoMapper;
using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.DTopic;
using ForumApi.Services.Interfaces;

namespace ForumApi.Services
{
    public class TopicService : ITopicService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _rep;

        public TopicService(IMapper mapper, IRepositoryManager rep)
        {
            _mapper = mapper;
            _rep = rep;
        }

        public async Task<Topic> Create(TopicDto topicDto)
        {
            var topic = _rep.Topic.Create(_mapper.Map<Topic>(topicDto));
            await _rep.Save();
            return topic;
        }
    }
}