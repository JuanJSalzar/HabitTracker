using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.ResponseDto;
using HabitsTracker.DTOs.UpdateDto;

namespace HabitsTracker.Services.IServices
{
    public interface IHabitService
    {
        Task<IEnumerable<ResponseHabitDto>> GetAllHabitsAsync();
        Task<ResponseHabitDto> GetHabitByIdAsync(int id);
        Task CreateHabitAsync(CreateHabitDto createHabitDto);
        Task UpdateHabitAsync(int id, UpdateHabitDto updateHabitDto);
        Task DeleteHabitAsync(int id);
    }
}