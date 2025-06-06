using AutoMapper;
using HabitsTracker.Models;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.DTOs.UpdateDto;
using HabitsTracker.DTOs.ResponseDto;
using HabitsTracker.Services.IServices;
using System.ComponentModel.DataAnnotations;
using HabitsTracker.Repository.GenericRepository;

namespace HabitsTracker.Services.ServicesImplementation
{
    public class HabitService(IGenericRepository<Habit> genericHabitRepository,
                            IGenericRepository<User> genericUserRepository,
                            IHabitRepository habitRepository,
                            IMapper mapper,
                            ILogger<HabitService> logger) : IHabitService
    {
        private readonly IGenericRepository<Habit> _genericHabitRepository = genericHabitRepository;
        private readonly IGenericRepository<User> _genericUserRepository = genericUserRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<HabitService> _logger = logger;
        private readonly IHabitRepository _habitRepository = habitRepository;
        public async Task<IEnumerable<ResponseHabitDto>> GetAllHabitsByUser(int userId)
        {
            _logger.LogInformation("Received request to retrieve all habits from the user: {userId}", userId);
            if (userId <= 0)
            {
                _logger.LogWarning("Invalid user ID received: {userId}", userId);
                throw new ValidationException("Invalid user ID.");
            }
            var user = await _genericUserRepository.GetByIdAsync(userId);
            if (user is null)
            {
                _logger.LogWarning("user with ID {userId} doesn't exist", userId);
                throw new KeyNotFoundException($"user with ID {userId} not found.");
            }
            var habits = _habitRepository.GetHabitsFromUser(userId);
            var habitsMapped = _mapper.Map<IEnumerable<ResponseHabitDto>>(habits);
            return habitsMapped;
        }
        public async Task<ResponseHabitDto> GetHabitByIdAsync(int userId, int id)
        {
            if (userId <= 0) throw new ArgumentException("User ID must be greater than zero.", nameof(userId));
            if (id <= 0) throw new ArgumentException("Habit ID must be greater than zero.", nameof(id));

            var user = await _genericUserRepository.GetByIdAsync(userId);
            if (user is null)
            {
                _logger.LogWarning("User with ID {UserId} doesn't exist.", userId);
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            
            var habit = await _habitRepository.GetHabitFromUserAndId(userId, id);
            if (habit is null)
            {
                _logger.LogWarning("Habit with ID {HabitId} not found for User {UserId}.", id, userId);
                throw new KeyNotFoundException($"Habit with ID {id} not found for user {userId}.");
            }
            return _mapper.Map<ResponseHabitDto>(habit);
        }
        public async Task CreateHabitAsync(CreateHabitDto createHabitDto, int userId)
        {
            _logger.LogInformation("Received request to create habit: {HabitName} for User ID: {userId}", createHabitDto.Name, userId);

            if (string.IsNullOrEmpty(createHabitDto.Name))
            {
                _logger.LogWarning("Validation failed: Name is required.");
                throw new ValidationException("Name is required");
            }

            if (userId <= 0)
            {
                _logger.LogWarning("Invalid user ID received: {userId}", userId);
                throw new ValidationException("Invalid user ID.");
            }

            var pendingHabits = _habitRepository.CountPendingHabitsByUser(userId);
            if (pendingHabits >= 5)
            {
                _logger.LogWarning("User {userId} has reached the limit of 5 pending habits.", userId);
                throw new InvalidOperationException("You already have 5 pending habits. Complete at least one before adding a new one.");
            }

            var newHabit = _mapper.Map<Habit>(createHabitDto);
            newHabit.UserId = userId;
            var createdHabit = await _genericHabitRepository.AddAsync(newHabit);

            _logger.LogInformation("Habit '{HabitName}' created successfully for User ID: {userId} with ID: {HabitId}",
                createdHabit.Name, userId, createdHabit.Id);
        } 
        public async Task UpdateHabitAsync(int id, UpdateHabitDto updateHabitDto, int userId)
        {
            _logger.LogInformation("Received request to update Habit ID: {HabitId}", id);
            if (id <= 0)
            {
                _logger.LogWarning("Validation failed: Invalid habit ID: {HabitId}", id);
                throw new ValidationException("Invalid habit ID.");
            }
            if(userId <= 0)
            {
                _logger.LogWarning("Validation failed: Invalid user ID: {userId}", userId);
                throw new ValidationException("Invalid user ID.");
            }
            if (updateHabitDto is null)
            {
                _logger.LogWarning("Validation failed: Habit update data is required.");
                throw new ArgumentNullException(nameof(updateHabitDto), "Habit update data is required.");
            }
            
            var habit = await _habitRepository.GetHabitFromUserAndId(userId, id);
            if (habit is null)
            {
                _logger.LogWarning("Habit with ID {HabitId} not found for User {UserId}.", id, userId);
                throw new KeyNotFoundException($"Habit with ID {id} not found for user {userId}.");
            }

            if (updateHabitDto.CurrentLog?.IsCompleted is not null && !Enum.IsDefined(typeof(Status), updateHabitDto.CurrentLog.IsCompleted))
            {
                throw new ValidationException("Invalid status value. Must be Uncompleted (0), OnGoing (1), Completed (2), or Pending (3).");
            }

            _mapper.Map(updateHabitDto, habit);
            habit.UpdatedAt = DateTime.UtcNow;

            await _genericHabitRepository.UpdateAsync(habit);

            _logger.LogInformation("Habit ID {HabitId} updated successfully.", id);
        } 
        public async Task DeleteHabitAsync(int id, int userId)
        {
            _logger.LogInformation("Received request to delete habit with ID: {HabitId}", id);

            if (userId <= 0)
            {
                _logger.LogWarning("Validation failed: Invalid user ID received: {userId}", userId);
                throw new ValidationException("Invalid user ID.");
            }
            if (id <= 0)
            {
                _logger.LogWarning("Invalid habit ID: {HabitId}", id);
                throw new ValidationException("Invalid habit ID.");
            }

            _logger.LogInformation("Fetching habit with ID: {HabitId}", id);
            var existingHabit = await _habitRepository.GetHabitFromUserAndId(userId, id);

            if (existingHabit is null)
            {
                _logger.LogWarning("Habit with ID {HabitId} not found.", id);
                throw new KeyNotFoundException($"Habit with ID {id} not found.");
            }

            _logger.LogInformation("Deleting habit with ID: {HabitId}", id);
            await _genericHabitRepository.DeleteAsync(id);

            _logger.LogInformation("Habit with ID {HabitId} successfully deleted.", id);
        } 

    }
}