using ForumApi.Data.Models;
using ForumApi.DTO.DTopic;
using ForumApi.DTO.Page;

namespace ForumApi.Services.Interfaces
{
    public interface ITopicService
    {
        Task<TopicResponse?> GetTopic(int id);
        Task<List<TopicElement>> GetTopics(int forumId, Page page);
        Task<Topic> Create(int authorId, TopicNew topicDto);
        Task<Topic> Update(int topicId, TopicDto topicDto);
        Task Delete(int topicId);
    }
}