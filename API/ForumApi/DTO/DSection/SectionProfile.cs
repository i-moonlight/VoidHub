using AutoMapper;
using ForumApi.Data.Models;

namespace ForumApi.DTO.DSection
{
    public class SectionProfile : Profile
    {
        public SectionProfile()
        {
            CreateMap<Section, SectionDto>();
            CreateMap<SectionDto, Section>();
        }
    }
}