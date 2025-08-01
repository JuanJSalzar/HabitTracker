using AutoMapper;
using HabitsTracker.Models;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.DTOs.ResponseDto;

namespace HabitsTracker.Mappings
{
    public class HabitLogMappingProfile : Profile
    {
        public HabitLogMappingProfile()
        {
            CreateMap<CreateHabitLogDto, HabitLog>();
            CreateMap<HabitLog, ResponseHabitLogDto>();
            CreateMap<UpdateHabitLogDto, HabitLog>();
        }
    }
}