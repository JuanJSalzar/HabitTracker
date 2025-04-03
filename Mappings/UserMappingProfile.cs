using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.ResponseDto;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.Models;

namespace HabitsTracker.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserDto, User>(); //Create user
            CreateMap<User, ResponseUserDto>(); //Get user
            CreateMap<UpdateUserDto, User>(); //Update User
        }
    }
}