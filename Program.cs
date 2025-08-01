using System.Text;
using System.Text.Json.Serialization;
using Azure.Identity;
using Azure.AI.OpenAI;
using HabitsTracker.Data;
using HabitsTracker.Models;
using HabitsTracker.Mappings;
using Microsoft.OpenApi.Models;
using HabitsTracker.Middlewares;
using HabitsTracker.Extensions.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using HabitsTracker.Services.IServices;
using HabitsTracker.Repository.Implementations;
using HabitsTracker.Repository.GenericRepository;
using HabitsTracker.Services.ServicesImplementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// --- Add basic services ---

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();

// --- Swagger ---

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Habit Tracker API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization, place the token:",
    };

    options.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    };

    options.AddSecurityRequirement(securityRequirement);

    options.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

// --- CORS ---

builder.Services.AddCors(options =>
{
    options.AddPolicy("habitsApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// --- Configuration ---

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection String" + "'DefaultConnection' not found");

// --- Database ---

builder.Services.AddDbContext<HabitTrackerContext>(options =>
    options.UseSqlServer(connectionString));

// --- Identity ---

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;

    options.User.RequireUniqueEmail = true;

})
.AddEntityFrameworkStores<HabitTrackerContext>()
.AddDefaultTokenProviders();

// --- AutoMapper ---

builder.Services.AddAutoMapper(typeof(HabitLogMappingProfile), typeof(HabitMappingProfile), typeof(UserMappingProfile));

// --- Scoped Services ---

builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IHabitRepository), typeof(HabitRepository));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IChatMessageRepository), typeof(ChatMessageRepository));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IHabitService, HabitService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// --- Singleton --- 

builder.Services.AddSingleton<AzureOpenAIClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var endpoint = configuration["AzureOpenAI:Endpoint"];
    if (string.IsNullOrEmpty(endpoint))
    {
        throw new InvalidOperationException("AzureOpenAI endpoint not configured.");
    }
    return new AzureOpenAIClient(new Uri(endpoint), new DefaultAzureCredential());
});

// --- JWT Authentication ---

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtSettings>(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT Key not configured."))
        ),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// --- HTTP Request Pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("habitsApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();