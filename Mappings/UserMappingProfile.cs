using AutoMapper;
using HabitsTracker.Models;
using HabitsTracker.DTOs.AuthDto;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.DTOs.ResponseDto;

namespace HabitsTracker.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<User, ResponseUserDto>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}