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
        Task<IEnumerable<ResponseHabitDto>> GetAllHabitsByUser(int userId);
        Task<ResponseHabitDto> GetHabitByIdAsync(int userId, int id);
        Task CreateHabitAsync(CreateHabitDto createHabitDto, int userId);
        Task UpdateHabitAsync(int id, UpdateHabitDto updateHabitDto, int userId);
        Task DeleteHabitAsync(int id, int userId);
    }
}