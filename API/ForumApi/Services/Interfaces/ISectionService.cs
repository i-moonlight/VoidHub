using ForumApi.Data.Models;
using ForumApi.DTO.DSection;

namespace ForumApi.Services.Interfaces
{
    public interface ISectionService
    {
        Task<List<SectionResponse>> GetSections();
        Task<Section> Create(SectionDto section);
    }
}