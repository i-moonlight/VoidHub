using ForumApi.Data.Models;
using ForumApi.DTO.DTopic;

namespace ForumApi.Services.Interfaces
{
    public interface ITopicService
    {
        Task<Topic> Create(TopicDto topicDto);
    }
}