using ForumApi.DTO.DPost;
using ForumApi.Extensions;
using ForumApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [ApiController]
    [Route("api/v1/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(PostDto postDto)
        {
            var post = await _postService.Create(User.Id(),postDto);
            return Ok(post);
        }
    }
}