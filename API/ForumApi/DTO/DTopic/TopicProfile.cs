using AutoMapper;
using ForumApi.Data.Models;

namespace ForumApi.DTO.DTopic
{
    public class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<TopicDto, Topic>();
            CreateMap<TopicNew, Topic>();
        }
    }
}