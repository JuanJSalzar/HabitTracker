using Microsoft.AspNetCore.Mvc;
using HabitsTracker.ActionFilters;
using HabitsTracker.DTOs.AuthDto;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.Services.IServices;

namespace HabitsTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUsersByIdAsync([FromRoute] int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] RegisterUserDto createUserDto)
        {
            await _userService.CreateUserAsync(createUserDto);
            return Ok(new { message = "User created successfully" });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto)
        {
            await _userService.UpdateUserAsync(id, updateUserDto);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok(new { message = "User deleted successfully" });
        }
    }
}
