using AutoMapper;
using ForumApi.Data.Models;

namespace ForumApi.DTO.DPost
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostDto, Post>();
        }        
    }
}