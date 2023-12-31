using ForumApi.Data.Models;
using ForumApi.DTO.DSection;
using ForumApi.Filters;
using ForumApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [ApiController]
    [Route("api/v1/sections")]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(
            ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sectionService.GetSections());
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        [BanFilter]
        public async Task<IActionResult> Create(SectionDto sectionDto)
        {
            var section = await _sectionService.Create(sectionDto);
            return Ok(section);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Role.Admin)]
        [BanFilter]
        public async Task<IActionResult> Update(int id, SectionDto sectionDto)
        {
            var section = await _sectionService.Update(id, sectionDto);
            return Ok(section);
        }
    }
}