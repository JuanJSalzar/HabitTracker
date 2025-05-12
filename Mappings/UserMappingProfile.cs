using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using HabitsTracker.DTOs.AuthDto;
using HabitsTracker.DTOs.ResponseDto;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.Models;

namespace HabitsTracker.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); //Create user
            CreateMap<User, ResponseUserDto>(); //Get user
            CreateMap<UpdateUserDto, User>(); //Update User
        }
    }
}