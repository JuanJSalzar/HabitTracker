using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.DTOs.AuthDto;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.PasswordDto;
using HabitsTracker.DTOs.ResponseDto;
using HabitsTracker.DTOs.UpdateDto;

namespace HabitsTracker.Services.IServices
{
    public interface IUserService
    {
        Task<ResponseUserDto?> GetMyProfile(int userId);
        Task UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task ChangePasswordAsync(int id, ChangePasswordDto changePasswordDto);
        Task DeleteUserAsync(int userId);
    }
}