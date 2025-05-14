using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.DTOs.AuthDto;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.ResponseDto;
using HabitsTracker.DTOs.UpdateDto;

namespace HabitsTracker.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<ResponseUserDto>> GetAllUsersAsync();
        Task<ResponseUserDto?> GetUserByIdAsync(int id);
        Task CreateUserAsync(RegisterUserDto createUserDto);
        Task UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task DeleteUserAsync(int id);
    }
}