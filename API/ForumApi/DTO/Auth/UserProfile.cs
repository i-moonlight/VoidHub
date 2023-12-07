using AutoMapper;
using ForumApi.Data.Models;
using ForumApi.DTO.Auth;

namespace ForumApi.DTO.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Account, User>();
            CreateMap<Account, AuthUser>();
        }
    }
}