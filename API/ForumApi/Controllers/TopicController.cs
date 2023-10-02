using ForumApi.DTO.DTopic;
using ForumApi.DTO.Page;
using ForumApi.Extensions;
using ForumApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [ApiController]
    [Route("api/v1/topics")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IPostService _postService;

        public TopicController(
            ITopicService topicService,
            IPostService postService)
        {
            _topicService = topicService;
            _postService = postService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopic(int id)
        {
            return Ok(await _topicService.GetTopic(id));
        }

        [HttpGet("{id}/posts")]
        public async Task<IActionResult> GetPostPage(int id, [FromQuery] Page page)
        {
            return Ok(await _postService.GetPostPage(id, page));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(TopicDto topicDto)
        {
            var topic = await _topicService.Create(User.GetId(), topicDto);
            return Ok(topic);
        }
    }
}