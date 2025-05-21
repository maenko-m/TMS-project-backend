using Microsoft.AspNetCore.Mvc;
using TmsSolution.Application.Dtos.Auth;
using TmsSolution.Application.Interfaces;

namespace TmsSolution.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                var result = await _authService.AuthenticateAsync(loginRequest.Email, loginRequest.Password);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }
        }
        
    }
}
