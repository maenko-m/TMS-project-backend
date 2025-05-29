using System.Security.Claims;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var id = user.FindFirst("sub")?.Value
               ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? user.FindFirst("user_id")?.Value;

            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var userId))
                throw new Exception("User ID not found in token.");

            return userId;
        }
    }
}
