using ForumApi.DTO.DForum;
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

        public ForumController(
            IForumService forumService)
        {
            _forumService = forumService;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(ForumDto forumDto)
        {
            var forum = await _forumService.Create(forumDto);
            return Ok(forum);
        }
    }
}