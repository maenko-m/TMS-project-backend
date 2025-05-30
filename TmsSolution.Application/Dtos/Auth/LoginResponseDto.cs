using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.Auth
{
    /// <summary>
    /// Data Transfer Object representing the result of a successful login.
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// The JWT or access token issued after successful authentication.
        /// </summary>
        /// <remarks>This token is used for authorizing subsequent requests.</remarks>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// The expiration date and time of the issued token (in UTC).
        /// </summary>
        /// <remarks>Clients should refresh or re-authenticate before this time to maintain access.</remarks>
        public DateTime ExpiresAt { get; set; }
    }
}
