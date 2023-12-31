using ForumApi.Data.Models;
using ForumApi.DTO.DPost;
using ForumApi.DTO.Page;
using ForumApi.Extensions;
using ForumApi.Filters;
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

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetPage(int id, [FromQuery] Offset page)
        {
            var posts = await _postService.GetPostComments(id, page);
            return Ok(posts);
        }

        [HttpPost]
        [Authorize]
        [BanFilter]
        public async Task<IActionResult> Create(PostDto postDto)
        {
            var post = await _postService.Create(User.GetId(), postDto);
            return Ok(post);
        }

        [HttpPut("{id}")]
        [Authorize]
        [BanFilter]
        [PermissionActionFilter<Post>]
        public async Task<IActionResult> Update(int id, PostDto postDto)
        {
            var post = await _postService.Update(id, postDto);
            return Ok(post);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Role.Admin},{Role.Moder}")]
        [BanFilter]
        public async Task<IActionResult> DeleteAsDmin(int id)
        {
            await _postService.Delete(id);
            return Ok();
        }
    }
}