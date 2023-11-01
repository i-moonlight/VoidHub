using ForumApi.Data.Models;
using ForumApi.DTO.DTopic;
using ForumApi.DTO.Page;
using ForumApi.Extensions;
using ForumApi.Filters;
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

        [HttpPost]
        [Authorize]
        [BanFilter]
        public async Task<IActionResult> Create(TopicNew topicDto)
        {
            var topic = await _topicService.Create(User.GetId(), topicDto);
            return Ok(topic);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{Role.Admin},{Role.Moder}")]
        [BanFilter]
        public async Task<IActionResult> Update(int id, TopicDto topicDto)
        {
            var topic = await _topicService.Update(id, topicDto);
            return Ok(topic);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Role.Admin},{Role.Moder}")]
        [BanFilter]
        public async Task<IActionResult> Delete(int id)
        {
            await _topicService.Delete(id);
            return Ok();
        }
    }
}