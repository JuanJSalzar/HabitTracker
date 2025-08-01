namespace HabitsTracker.DTOs.ResponseDto
{
    public record ResponseUserDto(
        int Id,
        string Name,
        string LastName,
        string Email,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}