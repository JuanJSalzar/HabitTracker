using System.Security.Claims;
using HabitsTracker.Utilities;
using Microsoft.AspNetCore.Mvc;
using HabitsTracker.Models.Bot;
using HabitsTracker.Services.IServices;
using Microsoft.AspNetCore.Authorization;

namespace HabitsTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("response")]
        public async Task<IActionResult> GetResponse([FromBody] Message message)
        {
            if (!UserHelper.TryGetUserId(User, out var userId)) return Unauthorized(new { message = "User identifier not found or invalid." });

            if (string.IsNullOrEmpty(message.Prompt)) return BadRequest("Message prompt cannot be null or empty.");
            
            var response = await _chatService.GetResponse(message.Prompt, userId); 
            return Ok(new { Response = response });
        }
    }
}