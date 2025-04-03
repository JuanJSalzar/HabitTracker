using AutoMapper;
using HabitsTracker.Models;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.ResponseDto;
using HabitsTracker.DTOs.UpdateDto;

namespace HabitsTracker.Mappings
{
    public class HabitLogMappingProfile : Profile
    {
        public HabitLogMappingProfile()
        {
            CreateMap<CreateHabitLogDto, HabitLog>(); //Create habit
            CreateMap<HabitLog, ResponseHabitLogDto>(); //Get habit
            CreateMap<UpdateHabitLogDto, HabitLog>(); //Update Habit
        }
    }
}