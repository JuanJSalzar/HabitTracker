# 🎯 HabitsTracker API

A comprehensive habit tracking API built with .NET 8, featuring user authentication, habit management, and AI-powered chat assistance for healthy lifestyle guidance.

## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Authentication](#authentication)
- [Database](#database)
- [AI Chat Assistant](#ai-chat-assistant)
- [Configuration](#configuration)
- [Development](#development)
- [Contributing](#contributing)

## ✨ Features

- 🔐 **User Authentication** - JWT-based authentication with registration and login
- 📊 **Habit Management** - Create, read, update, and delete habits with detailed logging
- 🤖 **AI Chat Assistant** - Azure OpenAI-powered chat for healthy habits guidance
- 📈 **Progress Tracking** - Track habit completion status, duration, and notes
- 🔒 **Secure API** - Protected endpoints with role-based access
- 📚 **API Documentation** - Comprehensive Swagger/OpenAPI documentation
- 🧪 **Clean Architecture** - Repository pattern, dependency injection, and separation of concerns

## 🛠️ Tech Stack

- **.NET 8** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Primary database
- **Azure OpenAI** - AI chat functionality
- **JWT Authentication** - Secure token-based authentication
- **AutoMapper** - Object-to-object mapping
- **Swagger/OpenAPI** - API documentation
- **Microsoft Identity** - User management

## 🚀 Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or full instance)
- Azure OpenAI subscription (for chat features)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/JuanJSalzar/HabitTracker.git
   cd HabitTracker
   ```

2. **Install dependencies**

   ```bash
   dotnet restore
   ```

3. **Set up User Secrets**

   ```bash
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=HabitsTrackerDb;Trusted_Connection=True;TrustServerCertificate=True;"
   dotnet user-secrets set "AzureOpenAI:Key" "your-azure-openai-key"
   dotnet user-secrets set "Jwt:SecretKey" "your-jwt-secret-key"
   ```

4. **Update Database**

   ```bash
   dotnet ef database update
   ```

5. **Run the application**

   ```bash
   dotnet run
   ```

6. **Access the API**
   - API: `https://localhost:5237`
   - Swagger UI: `https://localhost:5237/swagger`

## 📁 Project Structure

```
HabitsTracker/
├── Controllers/           # API Controllers
│   ├── AuthController.cs
│   ├── ChatController.cs
│   ├── HabitController.cs
│   └── UserController.cs
├── Models/               # Domain Models
│   ├── User.cs
│   ├── Habit.cs
│   ├── HabitLog.cs
│   └── Bot/
│       └── ChatMessageEntity.cs
├── DTOs/                 # Data Transfer Objects
│   ├── AuthDto/
│   ├── CreateDto/
│   ├── ResponseDto/
│   ├── UpdateDto/
│   └── PasswordDto/
├── Services/             # Business Logic
│   ├── IServices/
│   └── ServicesImplementation/
├── Repository/           # Data Access Layer
│   ├── GenericRepository/
│   └── Implementations/
├── Data/                 # Database Context
│   ├── HabitTrackerContext.cs
│   └── HabitTrackerContextFactory.cs
├── Mappings/             # AutoMapper Profiles
├── Extensions/           # JWT and other extensions
├── Middlewares/          # Custom middleware
├── SwaggerExamples/      # API documentation examples
└── Migrations/           # EF Core migrations
```

## 🔌 API Endpoints

### Authentication

- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - User login

### Users

- `GET /api/user` - Get all users
- `GET /api/user/{id}` - Get user by ID
- `PUT /api/user/{id}` - Update user
- `DELETE /api/user/{id}` - Delete user

### Habits

- `GET /api/habit` - Get all habits
- `GET /api/habit/{id}` - Get habit by ID
- `GET /api/habit/user/{userId}` - Get habits by user
- `POST /api/habit` - Create new habit
- `PUT /api/habit/{id}` - Update habit
- `DELETE /api/habit/{id}` - Delete habit

### Chat

- `POST /api/chat/response` - Get AI chat response
- `POST /api/chat/stream` - Stream AI chat response (SSE)

## 🔐 Authentication

The API uses JWT (JSON Web Tokens) for authentication:

1. **Register** or **Login** to get a JWT token
2. Include the token in the `Authorization` header: `Bearer <your-token>`
3. Tokens expire after 60 minutes (configurable)

### Example Authentication Flow

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "password123"
}
```

Response:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresAt": "2025-08-01T15:30:00Z"
}
```

## 🗄️ Database

The application uses Entity Framework Core with SQL Server:

### Main Entities

- **User** - Application users with Identity integration
- **Habit** - User habits with name, description, and user association
- **HabitLog** - Individual habit completion logs
- **ChatMessageEntity** - Chat conversation history

### Migrations

```bash
# Create new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## 🤖 AI Chat Assistant

The chat feature uses Azure OpenAI to provide personalized advice on healthy habits:

### Features

- Specialized in healthy habits advice (exercise, nutrition, rest, hydration)
- Conversation history maintained per user
- Daily message limits (5 messages per user per day)
- Streaming responses for real-time interaction

### Usage

```http
POST /api/chat/response
Authorization: Bearer <token>
Content-Type: application/json

{
  "userId": 1,
  "prompt": "How can I improve my morning routine?"
}
```

## ⚙️ Configuration

### App Settings Structure

```json
{
  "AzureOpenAI": {
    "Endpoint": "https://your-endpoint.cognitiveservices.azure.com/",
    "Key": "", // Set via User Secrets
    "Deployment": "habit-bot"
  },
  "Jwt": {
    "SecretKey": "", // Set via User Secrets
    "Issuer": "HabitsTrackerAPI",
    "Audience": "HabitsTrackerSPA",
    "ExpirationMinutes": 60
  },
  "ConnectionStrings": {
    "DefaultConnection": "" // Set via User Secrets
  }
}
```

### Environment Variables

For production, use environment variables or Azure Key Vault:

- `ConnectionStrings__DefaultConnection`
- `AzureOpenAI__Key`
- `Jwt__SecretKey`

## 🔧 Development

### Code Standards

- Follow C# naming conventions
- Use async/await for all database operations
- Implement proper error handling and logging
- Use DTOs for API contracts
- Follow repository pattern for data access

### Testing

```bash
# Run tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Debugging

The application includes:

- Exception handling middleware
- Comprehensive logging
- Swagger UI for API testing
- Development environment configurations

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Juan José Salazar**

- GitHub: [@JuanJSalzar](https://github.com/JuanJSalzar)
- Repository: [HabitTracker](https://github.com/JuanJSalzar/HabitTracker)

## 📞 Support

If you have any questions or need help, please open an issue on GitHub or contact the development team.

---

Made with ❤️ for building better habits
