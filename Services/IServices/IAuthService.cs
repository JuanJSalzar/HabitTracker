using HabitsTracker.DTOs.AuthDto;

namespace HabitsTracker.Services.IServices
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterUserDto createUserDto);
        Task<AuthResultDto> LoginAsync(LoginDto loginDto);

    }
}