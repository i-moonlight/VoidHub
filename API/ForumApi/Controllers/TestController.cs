using ForumApi.Data.Repository.Interfaces;
using ForumApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [ApiController]
    [Route("api/v1/test")]
    public class TestController : ControllerBase
    {
        private readonly IRepositoryManager _rep;

        public TestController(IRepositoryManager rep)
        {
            _rep = rep;
        }

        [HttpGet("ban")]
        [BanFilter]
        public async Task<IActionResult> TestBanFilter()
        {
            return Ok("You are not banned");
        }

    }
}