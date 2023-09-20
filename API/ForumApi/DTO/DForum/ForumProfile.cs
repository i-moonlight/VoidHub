using AutoMapper;
using ForumApi.Data.Models;

namespace ForumApi.DTO.DForum
{
    public class ForumProfile : Profile
    {
        public ForumProfile()
        {
            CreateMap<ForumDto, Forum>();
        }
    }
}