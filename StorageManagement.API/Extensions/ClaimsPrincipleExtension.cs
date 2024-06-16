using System.Security.Claims;

namespace StorageManagement.API.Extensions;

public static class ClaimsPrincipleExtension
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        var currentUser = user.FindFirst("preferred_username").Value;
        return currentUser;
    }

    public static int GetUserId(this ClaimsPrincipal user)
    {
        return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}
