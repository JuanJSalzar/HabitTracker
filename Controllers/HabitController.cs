using Microsoft.AspNetCore.Mvc;
using HabitsTracker.ActionFilters;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.Services.IServices;

namespace HabitsTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    public class HabitController(IHabitService habitService) : ControllerBase
    {
        private readonly IHabitService _habitService = habitService;

        [HttpGet]
        public async Task<IActionResult> GetAllHabitsAsync()
        {
            var habits = await _habitService.GetAllHabitsAsync();
            return Ok(habits);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHabitByIdAsync([FromRoute] int id)
        {
            var habit = await _habitService.GetHabitByIdAsync(id);
            return Ok(habit);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHabitAsync([FromBody] CreateHabitDto createHabitDto)
        {
            await _habitService.CreateHabitAsync(createHabitDto);
            return Ok(new { message = "Habit created successfully" });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateHabitAsync([FromRoute] int id, [FromBody] UpdateHabitDto updateHabitDto)
        {
            await _habitService.UpdateHabitAsync(id, updateHabitDto);
            return Ok(new { message = "Habit updated successfully" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHabitAsync([FromRoute] int id)
        {
            await _habitService.DeleteHabitAsync(id);
            return Ok(new { message = "Habit deleted successfully" });
        }
    }
}