using HabitsTracker.Models;
using HabitsTracker.DTOs.CreateDto;
using Swashbuckle.AspNetCore.Filters;

namespace HabitsTracker.SwaggerExamples.Habit
{
    public class CreateHabitDtoExample : IExamplesProvider<CreateHabitDto>
    {
        public CreateHabitDto GetExamples()
        {
            return new CreateHabitDto(
                "Wake Up Early",
                "This habit is being created to become part of the 5 AM club",
                new CreateHabitLogDto(
                    Status.Pending,
                    "Starting strong!",
                    DateTime.UtcNow,
                    TimeSpan.FromMinutes(30)
                )
            );
        }
    }
}