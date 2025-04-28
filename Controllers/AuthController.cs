using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HabitsTracker.ActionFilters;
using HabitsTracker.DTOs.CreateDto;
using HabitsTracker.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HabitsTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModel]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateUserDto createUserDto)
        {
            await _authService.RegisterAsync(createUserDto);
            return StatusCode(201, new { message = "Created successfully" });
        }
    }
}