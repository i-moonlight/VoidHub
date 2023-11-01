using AutoMapper;
using ForumApi.Data.Models;

namespace ForumApi.DTO.DBan
{
    public class BanProfile : Profile
    {
        public BanProfile()
        {
            CreateMap<BanDto, Ban>();
            CreateMap<Ban, BanResponse>();
        }        
    }
}