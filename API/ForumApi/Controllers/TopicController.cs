using ForumApi.DTO.DTopic;
using ForumApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [ApiController]
    [Route("api/v1/topics")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopicDto topicDto)
        {
            var topic = await _topicService.Create(topicDto);
            return Ok(topic);
        }
    }
}