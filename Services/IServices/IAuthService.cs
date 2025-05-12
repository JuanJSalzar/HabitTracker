using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.DTOs.AuthDto;
using HabitsTracker.DTOs.CreateDto;

namespace HabitsTracker.Services.IServices
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterUserDto createUserDto);
        Task<AuthResultDto> LoginAsync(LoginDto loginDto);

    }
}