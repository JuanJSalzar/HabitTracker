using AutoMapper;
using HabitsTracker.Models;
using HabitsTracker.DTOs.UpdateDto;
using Microsoft.AspNetCore.Identity;
using HabitsTracker.DTOs.PasswordDto;
using HabitsTracker.DTOs.ResponseDto;
using HabitsTracker.Services.IServices;
using System.ComponentModel.DataAnnotations;
using HabitsTracker.Repository.GenericRepository;

namespace HabitsTracker.Services.ServicesImplementation
{
    public class UserService(IGenericRepository<User> genericRepository, 
                             IPasswordHasher<User> passwordHasher,
                             IUserRepository userRepository, 
                             IMapper mapper, 
                             ILogger<UserService> logger) : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository = genericRepository;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<UserService> _logger = logger;

        public async Task<ResponseUserDto?> GetMyProfile(int userId)
        {
            _logger.LogInformation("Received request to retrieve user with ID: {userId}", userId);

            if (userId <= 0)
            {
                _logger.LogWarning("Invalid user ID received: {userId}", userId);
                throw new ValidationException("Invalid user ID.");
            }

            var user = await _genericRepository.GetByIdAsync(userId);

            if (user is not null) return _mapper.Map<ResponseUserDto>(user);
            
            _logger.LogWarning("User with ID {userId} not found.", userId);
            throw new KeyNotFoundException($"User with ID {userId} not found.");

        }
        public async Task UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            _logger.LogInformation("Received request to update user with ID: {userId}", userId);

            if (userId <= 0)
            {
                _logger.LogWarning("Invalid user ID: {userId}", userId);
                throw new ValidationException("Invalid user ID.");
            }

            if (updateUserDto is null)
            {
                _logger.LogWarning("UpdateUserDto is null for user ID: {UserId}", userId);
                throw new ArgumentNullException(nameof(updateUserDto), "User update data is required.");
            }

            if (await _userRepository.ExistsByEmailAsync(updateUserDto.Email))
            {
                _logger.LogWarning("Email '{Email}' is already taken. Cannot update user.", updateUserDto.Email);
                throw new ValidationException("Email is already taken.");
            }

            var user = await _genericRepository.GetByIdAsync(userId);
            if (user is null)
            {
                _logger.LogWarning("User not found: {userId}", userId);
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            _mapper.Map(updateUserDto, user);
            
            user.Email = updateUserDto.Email;
            user.NormalizedEmail = updateUserDto.Email.ToUpperInvariant();
            user.UserName = updateUserDto.Email;
            user.NormalizedUserName = updateUserDto.Email.ToUpperInvariant();
            user.UpdatedAt = DateTime.UtcNow;
            
            await _genericRepository.UpdateAsync(user);

            _logger.LogInformation("User updated successfully: {userId}", userId);
        }

        public async Task ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            if (!changePasswordDto.NewPassword.Equals(changePasswordDto.ConfirmNewPassword))
            {
                throw new ValidationException("Passwords do not match.");
            }

            var user = await _genericRepository.GetByIdAsync(userId);
            if(user is null) throw new KeyNotFoundException($"User with ID {userId} not found.");

            if (user.PasswordHash == null)
                throw new InvalidOperationException("User has no password set.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, changePasswordDto.CurrentPassword);
            if (result == PasswordVerificationResult.Failed)
                throw new ValidationException("Current password is incorrect.");

            user.PasswordHash = _passwordHasher.HashPassword(user, changePasswordDto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _genericRepository.UpdateAsync(user);
        }
        public async Task DeleteUserAsync(int userId)
        {
            _logger.LogInformation("Received request to delete user with ID: {userId}", userId);

            if (userId <= 0)
            {
                _logger.LogWarning("Invalid user ID: {userId}", userId);
                throw new ValidationException("Invalid user ID.");
            }

            _logger.LogInformation("Fetching user with ID: {userId}", userId);
            var existingUser = await _genericRepository.GetByIdAsync(userId);

            if (existingUser == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            _logger.LogInformation("Deleting user with ID: {userId}", userId);
            await _genericRepository.DeleteAsync(userId);

            _logger.LogInformation("User with ID {userId} successfully deleted.", userId);
        }
    }
}