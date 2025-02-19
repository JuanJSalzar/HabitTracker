using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.ResponseDto;
using HabitsTracker.DTOs.UpdateDto;

namespace HabitsTracker.Services.IServices
{
    public interface IUserService
    {
        Task<ResponseUserDto> GetAllUsersAsyc();
        Task<ResponseUserDto> GetUsersByIdAsync(int id);
        Task<CreateUserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UpdateUserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task DeleteUserAsync(int id);
    }
}