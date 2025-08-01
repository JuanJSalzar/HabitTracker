using HabitsTracker.DTOs.UpdateDto;
using Swashbuckle.AspNetCore.Filters;

namespace HabitsTracker.SwaggerExamples.User
{
    public class UpdateUserDtoExample : IExamplesProvider<UpdateUserDto>
    {
        public UpdateUserDto GetExamples()
        {
            return new UpdateUserDto(
                Name: "John",
                LastName: "Doe",
                Email: "john.doe@example.com"
            );
        }
    }
}