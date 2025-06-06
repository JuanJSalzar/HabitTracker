using System.Security.Claims;
using HabitsTracker.Utilities;
using Microsoft.AspNetCore.Mvc;
using HabitsTracker.ActionFilters;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.Services.IServices;
using Microsoft.AspNetCore.Authorization;

namespace HabitsTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    [Authorize]
    public class HabitController(IHabitService habitService) : ControllerBase
    {
        private readonly IHabitService _habitService = habitService;

        [HttpGet]
        public async Task<IActionResult> GetHabitFromUserAsync()
        {
            if (!UserHelper.TryGetUserId(User, out var userId)) return Unauthorized(new { message = "User identifier not found or invalid." });
            
            var habit = await _habitService.GetAllHabitsByUser(userId);
            return Ok(habit);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHabitByIdAsync([FromRoute] int id)
        {
            if (!UserHelper.TryGetUserId(User, out var userId)) return Unauthorized(new { message = "User identifier not found or invalid." });

            var habit = await _habitService.GetHabitByIdAsync(userId, id);
            return Ok(habit);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHabitAsync([FromBody] CreateHabitDto createHabitDto)
        {
            if (!UserHelper.TryGetUserId(User, out var userId)) return Unauthorized(new { message = "User identifier not found or invalid." });

            await _habitService.CreateHabitAsync(createHabitDto, userId);
            return Ok(new { message = "Habit created successfully" });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateHabitAsync([FromRoute] int id, [FromBody] UpdateHabitDto updateHabitDto)
        {
            if (!UserHelper.TryGetUserId(User, out var userId)) return Unauthorized(new { message = "User identifier not found or invalid." });

            await _habitService.UpdateHabitAsync(id, updateHabitDto, userId);
            return Ok(new { message = "Habit updated successfully" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHabitAsync([FromRoute] int id)
        {
            if (!UserHelper.TryGetUserId(User, out var userId)) return Unauthorized(new { message = "User identifier not found or invalid." });

            await _habitService.DeleteHabitAsync(id, userId);
            return NoContent();
        }
    }
}