using System.Security.Claims;
using HabitsTracker.Utilities;
using Microsoft.AspNetCore.Mvc;
using HabitsTracker.ActionFilters;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.DTOs.PasswordDto;
using HabitsTracker.Services.IServices;

namespace HabitsTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]/me")]
    [ValidateModel]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetMyProfileAsync()
        {
            if (!UserHelper.TryGetUserId(User, out var userId)) return Unauthorized(new { message = "User identifier not found or invalid." });
            var user = await _userService.GetMyProfile(userId);
            return Ok(user);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDto updateUserDto)
        {
            if (!UserHelper.TryGetUserId(User, out var userId)) return Unauthorized(new { message = "User identifier not found or invalid." });

            await _userService.UpdateUserAsync(userId, updateUserDto);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpPut("password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!UserHelper.TryGetUserId(User, out var userId)) return Unauthorized(new { message = "User identifier not found or invalid." });
            
            await _userService.ChangePasswordAsync(userId, changePasswordDto);
            return Ok(new { message = "Password changed successfully" });
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            if (!UserHelper.TryGetUserId(User, out var userId)) return Unauthorized(new { message = "User identifier not found or invalid." });

            await _userService.DeleteUserAsync(userId);
            return Ok(new { message = "User deleted successfully" });
        }
                                        
        private bool TryGetUserId(out int userId)
        {
            userId = 0;
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return !string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out userId);
        }
    }
}
