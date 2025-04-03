using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.UpdateDto;
using Swashbuckle.AspNetCore.Filters;

namespace HabitsTracker.SwaggerExamples.User
{
    public class CreateUserDtoExample : IExamplesProvider<CreateUserDto>
    {
        public CreateUserDto GetExamples()
        {
            return new CreateUserDto(
                Name: "John",
                LastName: "Doe",
                Email: "john.doe@example.com"
            );
        }
    }
}