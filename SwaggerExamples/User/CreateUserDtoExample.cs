using HabitsTracker.DTOs.AuthDto;
using Swashbuckle.AspNetCore.Filters;

namespace HabitsTracker.SwaggerExamples.User
{
    public class CreateUserDtoExample : IExamplesProvider<RegisterUserDto>
    {
        public RegisterUserDto GetExamples()
        {
            return new RegisterUserDto(
                Name: "John",
                LastName: "Doe",
                Email: "john.doe@example.com",
                Password: "m#P52s@ap$V"
            );
        }
    }
}