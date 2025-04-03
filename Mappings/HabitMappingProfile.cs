using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.ResponseDto;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.Models;

namespace HabitsTracker.Mappings
{
    public class HabitMappingProfile : Profile
    {
        public HabitMappingProfile()
        {
            CreateMap<CreateHabitDto, Habit>(); //Create habit
            CreateMap<Habit, ResponseHabitDto>(); //Get habit
            CreateMap<UpdateHabitDto, Habit>(); //Update Habit
        }
    }
}