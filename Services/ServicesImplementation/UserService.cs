using System.ComponentModel.DataAnnotations;
using System.Data;
using AutoMapper;
using Azure;
using HabitsTracker.DTOs.AuthDto;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.ResponseDto;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.Models;
using HabitsTracker.Repository.GenericRepository;
using HabitsTracker.Repository.Implementations;
using HabitsTracker.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HabitsTracker.Services.ServicesImplementation
{
    public class UserService(IGenericRepository<User> genericRepository, IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger) : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository = genericRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<UserService> _logger = logger;

        public async Task<IEnumerable<ResponseUserDto>> GetAllUsersAsync()
        {
            _logger.LogInformation("Received request to retrieve all users");

            _logger.LogInformation("Fetching all users from the database...");
            var users = await _genericRepository
                        .GetQueryable()
                        .Include(u => u.Habits)
                        .ToListAsync();

            if (users is null || users.Count == 0)
            {
                _logger.LogWarning("No users found in the database.");
                throw new KeyNotFoundException("No users found.");
            }

            _logger.LogDebug("Mapping {UserCount} users to DTOs...", users.Count);
            var usersMapped = _mapper.Map<IEnumerable<ResponseUserDto>>(users);

            _logger.LogInformation("Successfully fetched {Count} users.", usersMapped.Count());
            return usersMapped;
        }
        public async Task<ResponseUserDto?> GetUserByIdAsync(int id)
        {
            _logger.LogInformation("Received request to retrieve user with ID: {UserId}", id);

            if (id <= 0)
            {
                _logger.LogWarning("Invalid user ID received: {UserId}", id);
                throw new ValidationException("Invalid user ID.");
            }

            var user = await _genericRepository
                        .GetQueryable()
                        .Include(u => u.Habits)
                        .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", id);
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            return _mapper.Map<ResponseUserDto>(user);
        }
        public async Task CreateUserAsync(RegisterUserDto createUserDto)
        {
            _logger.LogInformation("Received request to create user with Name: {Name}", createUserDto.Name);

            if (string.IsNullOrEmpty(createUserDto.Email))
            {
                _logger.LogWarning("Validation failed: Email is required.");
                throw new ValidationException("Email is required");
            }

            if (await _userRepository.ExistsByEmailAsync(createUserDto.Email))
            {
                _logger.LogWarning("Email '{Email}' is already taken. Cannot create user.", createUserDto.Email);
                throw new ValidationException("Email is already taken");
            }

            var newUser = _mapper.Map<User>(createUserDto);
            var createdUser = await _genericRepository.AddAsync(newUser);

            _logger.LogInformation("User created successfully with ID: {UserId}", createdUser.Id);
        }
        public async Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            _logger.LogInformation("Received request to update user with ID: {UserId}", id);

            if (id <= 0)
            {
                _logger.LogWarning("Invalid user ID: {UserId}", id);
                throw new ValidationException("Invalid user ID.");
            }

            if (updateUserDto is null)
            {
                _logger.LogWarning("UpdateUserDto is null for user ID: {UserId}", id);
                throw new ArgumentNullException(nameof(updateUserDto), "User update data is required.");
            }

            if (await _userRepository.ExistsByEmailAsync(updateUserDto.Email))
            {
                _logger.LogWarning("Email '{Email}' is already taken. Cannot update user.", updateUserDto.Email);
                throw new ValidationException("Email is already taken.");
            }

            var user = await _genericRepository.GetByIdAsync(id);
            if (user is null)
            {
                _logger.LogWarning("User not found: {UserId}", id);
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            _mapper.Map(updateUserDto, user);
            user.UpdatedAt = DateTime.UtcNow;
            await _genericRepository.UpdateAsync(user);

            _logger.LogInformation("User updated successfully: {UserId}", id);
        }
        public async Task DeleteUserAsync(int id)
        {
            _logger.LogInformation("Received request to delete user with ID: {UserId}", id);

            if (id <= 0)
            {
                _logger.LogWarning("Invalid user ID: {UserId}", id);
                throw new ValidationException("Invalid user ID.");
            }

            _logger.LogInformation("Fetching user with ID: {UserId}", id);
            var existingUser = await _genericRepository.GetByIdAsync(id);

            if (existingUser == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", id);
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            _logger.LogInformation("Deleting user with ID: {UserId}", id);
            await _genericRepository.DeleteAsync(id);

            _logger.LogInformation("User with ID {UserId} successfully deleted.", id);
        }
    }
}