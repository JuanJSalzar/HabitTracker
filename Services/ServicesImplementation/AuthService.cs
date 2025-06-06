using AutoMapper;
using HabitsTracker.Models;
using HabitsTracker.DTOs.AuthDto;
using HabitsTracker.Extensions.JWT;
using Microsoft.AspNetCore.Identity;
using HabitsTracker.Services.IServices;
using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Services.ServicesImplementation
{
    public class AuthService(IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ILogger<AuthService> logger) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AuthService> _logger = logger;
        public async Task RegisterAsync(RegisterUserDto createUserDto)
        {
            _logger.LogInformation("Starting user registration for email: {Email}", createUserDto.Email);

            if (string.IsNullOrEmpty(createUserDto.Email) || string.IsNullOrEmpty(createUserDto.Password))
            {
                _logger.LogWarning("Validation failed: Email or Password is empty.");
                throw new ValidationException("Email or Password are required");
            }

            var existingUser = await _userManager.FindByEmailAsync(createUserDto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Registration failed: Email {Email} is already taken.", createUserDto.Email);
                throw new ValidationException("Email is already taken");
            }

            var newUser = _mapper.Map<User>(createUserDto);

            var createdUser = await _userManager.CreateAsync(newUser, createUserDto.Password);

            if (createdUser.Succeeded)
            {
                _logger.LogInformation("User created successfully: {UserId}", newUser.Id);

                await _signInManager.SignInAsync(newUser, isPersistent: false);

                _logger.LogInformation("User {UserId} signed in after registration.", newUser.Id);
            }
            else
            {
                var errors = string.Join(", ", createdUser.Errors.Select(e => e.Description));
                _logger.LogError("User creation failed for email {Email}: {Errors}", createUserDto.Email, errors);
                throw new ValidationException($"User creation failed: {errors}");
            }
        }

        public async Task<AuthResultDto> LoginAsync(LoginDto loginDto)
        {
            _logger.LogInformation("Login attempt for email: {Email}", loginDto.Email);

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                _logger.LogWarning("Login failed for email: {Email}", loginDto.Email);
                throw new UnauthorizedAccessException("Invalid Credentials");
            }

            var jwtTokenGenerator = new JwtTokenGenerator(_configuration);
            var token = jwtTokenGenerator.GenerateToken(user);

            _logger.LogInformation("Login successful for user ID: {UserId}", user.Id);

            return new AuthResultDto(Token: token, ExpiresAt: DateTime.Now.AddHours(1));
        }
    }
}