using System.Security.Claims;

namespace AdemideInfoWebsite.Server.Controllers;

public static class ClaimsExtensions
{
    /// <summary>
    /// Extracts the current user's ID from the JWT token claims
    /// This is used in endpoints to identify which user is making the request
    /// </summary>
    /// <param name="user">The ClaimsPrincipal from the HTTP context (contains JWT claims)</param>
    /// <returns>The user's GUID identifier</returns>
    /// <exception cref="InvalidOperationException">Thrown if the user ID claim is missing or invalid</exception>
    public static Guid CurrentUserId(this ClaimsPrincipal user)
    {

        foreach (var claim in user.Claims)
        {
            Console.WriteLine($"{claim.Type} = {claim.Value}");
        }

        // Find the NameIdentifier claim which contains the user ID
        // This claim is set when the JWT token is created during login/registration
        var rawId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        // Try to parse the ID as a GUID
        if (Guid.TryParse(rawId, out var userId))
        {
            Console.WriteLine($"[CLAIMS] Extracted user ID from token: {userId}");
            return userId;
        }

        // If parsing fails or claim is missing, throw an exception
        Console.WriteLine("[CLAIMS] ERROR: User ID claim is missing or invalid");
        throw new InvalidOperationException("User id claim is missing.");
    }
}
