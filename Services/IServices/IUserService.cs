using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.DTOs.PasswordDto;
using HabitsTracker.DTOs.ResponseDto;

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