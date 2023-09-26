using ForumApi.DTO.DForum;
using ForumApi.DTO.Page;
using ForumApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [ApiController]
    [Route("api/v1/forums")]
    public class ForumController : ControllerBase
    {
        private readonly IForumService _forumService;
        private readonly ITopicService _topicService;

        public ForumController(
            IForumService forumService,
            ITopicService topicService)
        {
            _forumService = forumService;
            _topicService = topicService;
        }

        [HttpGet("{forumId}")]
        public async Task<IActionResult> GetForum(int forumId)
        {
            return Ok(await _forumService.Get(forumId));
        }

        [HttpGet("{forumId}/topics")]
        public async Task<IActionResult> GetTopicsPage(int forumId, [FromQuery] Page page)
        {
            return Ok(await _topicService.GetTopics(forumId, page));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(ForumDto forumDto)
        {
            var forum = await _forumService.Create(forumDto);
            return Ok(forum);
        }
    }
}