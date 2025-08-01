using HabitsTracker.Models;
using HabitsTracker.DTOs.UpdateDto;
using Swashbuckle.AspNetCore.Filters;

namespace HabitsTracker.SwaggerExamples.Habit
{
    public class UpdateHabitDtoExample : IExamplesProvider<UpdateHabitDto>
    {
        public UpdateHabitDto GetExamples()
        {
            return new UpdateHabitDto(
                "Workout",
                "Daily morning workout for 30 minutes",
                new UpdateHabitLogDto(
                    Status.Completed,
                    "Feeling great after today's session!",
                    DateTime.UtcNow.AddDays(-1),
                    TimeSpan.FromMinutes(45)
                )
            );
        }
    }

}