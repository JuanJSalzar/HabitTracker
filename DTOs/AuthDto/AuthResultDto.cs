namespace HabitsTracker.DTOs.AuthDto
{
    public record AuthResultDto(
        string Token,
        DateTime ExpiresAt
    );
}