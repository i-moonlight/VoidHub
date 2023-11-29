using ForumApi.Data.Models;
using ForumApi.DTO.DTopic;
using ForumApi.DTO.Page;

namespace ForumApi.Services.Interfaces
{
    public interface ITopicService
    {
        /// <summary>
        /// Get topic info, first post and first comments
        /// </summary>
        Task<TopicResponse?> GetTopic(int id);
        /// <summary>
        /// Load topics on forum
        /// </summary>
        Task<List<TopicElement>> GetTopics(int forumId, Page page);
        /// <summary>
        /// Get topics info and first post
        /// </summary>
        /// <param name="time">Tmestamp from first load to not load newest topics</param>
        /// <returns></returns>
        Task<List<TopicResponse>> GetTopics(Offset offset, DateTime time);
        Task<Topic> Create(int authorId, TopicNew topicDto);
        Task<Topic> Update(int topicId, TopicDto topicDto);
        Task Delete(int topicId);
    }
}