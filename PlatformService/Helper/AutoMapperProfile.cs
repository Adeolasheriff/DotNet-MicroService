using AutoMapper;
using PlatformService.Dto;
using PlatformService.Models;

namespace PlatformService.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Platform, PlatformReadDto>().ReverseMap();
            CreateMap<PlatformCreateDto, Platform>().ReverseMap();
        }
    }
}