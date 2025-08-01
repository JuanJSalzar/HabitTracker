using System.Security.Claims;

namespace HabitsTracker.Utilities;

public static class UserHelper
{
    public static bool TryGetUserId(ClaimsPrincipal user, out int userId)
    {
        userId = 0;
        var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return !string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out userId);
    }
}