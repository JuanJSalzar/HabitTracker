using HabitsTracker.Data;
using HabitsTracker.Mappings;
using HabitsTracker.Middlewares;
using HabitsTracker.Models;
using HabitsTracker.Repository.GenericRepository;
using HabitsTracker.Repository.Implementations;
using HabitsTracker.Services.IServices;
using HabitsTracker.Services.ServicesImplementation;
using HabitsTracker.SwaggerExamples.Habit;
using HabitsTracker.SwaggerExamples.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Habit Tracker API", Version = "v1" });

    options.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateUserDtoExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<UpdateUserDtoExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateHabitDtoExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<UpdateHabitDtoExample>();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection String" + "'DefaultConnection' not found");

builder.Services.AddDbContext<HabitTrackerContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped((typeof(IUserRepository)), typeof(UserRepository));
builder.Services.AddScoped((typeof(IHabitRepository)), typeof(HabitRepository));

builder.Services.AddAutoMapper(typeof(HabitLogMappingProfile), typeof(HabitMappingProfile), typeof(UserMappingProfile));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHabitService, HabitService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

