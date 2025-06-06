using AutoMapper;
using HabitsTracker.Models;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.DTOs.ResponseDto;

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