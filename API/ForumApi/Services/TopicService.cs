using AutoMapper;
using ForumApi.Data.Models;
using ForumApi.Data.Repository.Extensions;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.Auth;
using ForumApi.DTO.DPost;
using ForumApi.DTO.DTopic;
using ForumApi.DTO.Page;
using ForumApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Topic> Create(int authorId, TopicDto topicDto)
        {
            var topic = _mapper.Map<Topic>(topicDto);
            topic.AccountId = authorId;

            var post = new Post
            {
                Content = topicDto.Content, 
                AccountId = authorId
            };

            topic.Posts.Add(post);

            _rep.Topic.Create(topic);
            await _rep.Save();
            return topic;
        }

        public async Task<List<TopicListElement>> GetTopics(int forumId, Page page)
        {
            return await _rep.Topic
                .FindByCondition(t => t.DeletedAt == null)
                .Include(t => t.Author)
                .Include(t => t.Posts.Where(p => p.DeletedAt == null).OrderBy(p => p.CreatedAt))
                .ThenInclude(p => p.Author)
                .Select(p => new TopicListElement
                {
                    Id = p.Id,
                    Title = p.Title,
                    CreatedAt = p.CreatedAt,
                    Content = p.Posts.FirstOrDefault() == null ? "" : p.Posts.FirstOrDefault().Content,
                    msgsCount = p.Posts.Count,
                    Author = _mapper.Map<User>(p.Author),
                    LastPost = _mapper.Map<LastPost>(p.Posts.OrderByDescending(p => p.CreatedAt).FirstOrDefault())
                })
                .TakePage(page)
                .ToListAsync();
        }
    }
}