using Microsoft.AspNetCore.Mvc;
using TmsSolution.Application.Dtos.Auth;
using TmsSolution.Application.Interfaces;

namespace TmsSolution.Presentation.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a user with the provided email and password.
        /// </summary>
        /// <param name="loginRequest">
        /// An instance of <see cref="LoginRequestDto"/> containing the user's email and password.
        /// </param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/>:
        /// <list type="bullet">
        /// <item><description><see cref="OkObjectResult"/> with authentication result (e.g. token) if credentials are valid.</description></item>
        /// <item><description><see cref="UnauthorizedResult"/> with an error message if the email or password is incorrect.</description></item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// This endpoint does not require prior authentication. It is used to obtain an access token or session.
        /// </remarks>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                var result = await _authService.AuthenticateAsync(loginRequest.Email, loginRequest.Password);
                return Ok(result);
            }
            catch (Exception)
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }
        }
        
    }
}
