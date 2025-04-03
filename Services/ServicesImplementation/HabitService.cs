using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class HabitService(IGenericRepository<Habit> genericRepository, IHabitRepository habitRepository, IMapper mapper, ILogger<HabitService> logger) : IHabitService
    {
        private readonly IGenericRepository<Habit> _genericRepository = genericRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<HabitService> _logger = logger;
        private readonly IHabitRepository _habitRepository = habitRepository;

        public async Task<IEnumerable<ResponseHabitDto>> GetAllHabitsAsync()
        {
            _logger.LogInformation("Received request to retrieve all habits");

            _logger.LogInformation("Fetching all habits from the database...");
            var habits = await _genericRepository.GetAllAsync();

            if (habits is null || !habits.Any())
            {
                _logger.LogWarning("No habits found in the database.");
                throw new KeyNotFoundException("No habits found.");
            }

            _logger.LogDebug("Mapping {HabitsCount} habits to DTOs...", habits.Count());
            var habitsMapped = _mapper.Map<IEnumerable<ResponseHabitDto>>(habits);

            _logger.LogInformation("Successfully fetched {Count} habits", habitsMapped.Count());
            return habitsMapped;
        }
        public async Task<ResponseHabitDto> GetHabitByIdAsync(int id)
        {
            _logger.LogInformation("Received request to retrieve habit with ID: {HabitId}", id);

            if (id <= 0)
            {
                _logger.LogWarning("Invalid habit ID received: {HabitId}", id);
                throw new ValidationException("Invalid habit ID.");
            }

            var habit = await _genericRepository.GetByIdAsync(id);
            if (habit is null)
            {
                _logger.LogWarning("Habit with ID {HabitId} not found.", id);
                throw new KeyNotFoundException($"Habit with ID {id} not found.");
            }

            return _mapper.Map<ResponseHabitDto>(habit);
        }
        public async Task CreateHabitAsync(CreateHabitDto createHabitDto)
        {
            _logger.LogInformation("Received request to create habit: {HabitName} for User ID: {UserId}", createHabitDto.Name, createHabitDto.UserId);

            if (string.IsNullOrEmpty(createHabitDto.Name))
            {
                _logger.LogWarning("Validation failed: Name is required.");
                throw new ValidationException("Name is required");
            }

            if (createHabitDto.UserId <= 0)
            {
                _logger.LogWarning("Invalid user ID received: {UserId}", createHabitDto.UserId);
                throw new ValidationException("Invalid user ID.");
            }

            var pendingHabits = _habitRepository.CountPendingHabitsByUser(createHabitDto.UserId);
            if (pendingHabits >= 5)
            {
                _logger.LogWarning("User {UserId} has reached the limit of 5 pending habits.", createHabitDto.UserId);
                throw new InvalidOperationException("You already have 5 pending habits. Complete at least one before adding a new one.");
            }

            var newHabit = _mapper.Map<Habit>(createHabitDto);
            var createdHabit = await _genericRepository.AddAsync(newHabit);

            _logger.LogInformation("Habit '{HabitName}' created successfully for User ID: {UserId} with ID: {HabitId}",
                createdHabit.Name, createdHabit.UserId, createdHabit.Id);
        }
        public async Task UpdateHabitAsync(int id, UpdateHabitDto updateHabitDto)
        {
            _logger.LogInformation("Received request to update Habit ID: {HabitId}", id);

            if (id <= 0)
            {
                _logger.LogWarning("Validation failed: Invalid habit ID: {HabitId}", id);
                throw new ValidationException("Invalid habit ID.");
            }

            if (updateHabitDto is null)
            {
                _logger.LogWarning("Validation failed: Habit update data is required.");
                throw new ArgumentNullException(nameof(updateHabitDto), "Habit update data is required.");
            }

            var habit = await _genericRepository.GetByIdAsync(id);
            if (habit is null)
            {
                _logger.LogWarning("Habit with ID {HabitId} not found.", id);
                throw new KeyNotFoundException($"Habit with ID {id} not found.");
            }

            if (updateHabitDto.CurrentLog?.IsCompleted is not null && !Enum.IsDefined(typeof(Status), updateHabitDto.CurrentLog.IsCompleted))
            {
                throw new ValidationException("Invalid status value. Must be Uncompleted (0), OnGoing (1), Completed (2), or Pending (3).");
            }

            _mapper.Map(updateHabitDto, habit);
            habit.UpdatedAt = DateTime.UtcNow;

            await _genericRepository.UpdateAsync(habit);

            _logger.LogInformation("Habit ID {HabitId} updated successfully.", id);
        }
        public async Task DeleteHabitAsync(int id)
        {
            _logger.LogInformation("Received request to delete habit with ID: {HabitId}", id);

            if (id <= 0)
            {
                _logger.LogWarning("Invalid habit ID: {HabitId}", id);
                throw new ValidationException("Invalid habit ID.");
            }

            _logger.LogInformation("Fetching habit with ID: {HabitId}", id);
            var existingHabit = await _genericRepository.GetByIdAsync(id);

            if (existingHabit is null)
            {
                _logger.LogWarning("Habit with ID {HabitId} not found.", id);
                throw new KeyNotFoundException($"Habit with ID {id} not found.");
            }

            _logger.LogInformation("Deleting habit with ID: {HabitId}", id);
            await _genericRepository.DeleteAsync(id);

            _logger.LogInformation("Habit with ID {HabitId} successfully deleted.", id);
        }
    }
}