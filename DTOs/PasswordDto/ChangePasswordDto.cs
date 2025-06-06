using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.DTOs.PasswordDto;

public record ChangePasswordDto(
    [Required]
    string CurrentPassword, 
    [Required]
    string NewPassword, 
    [Required]
    string ConfirmNewPassword);