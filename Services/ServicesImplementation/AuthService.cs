using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.Models;
using HabitsTracker.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HabitsTracker.Services.ServicesImplementation
{
    public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ILogger<AuthService> logger) : IAuthService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AuthService> _logger = logger;
        public async Task RegisterAsync(CreateUserDto createUserDto)
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
    }
}