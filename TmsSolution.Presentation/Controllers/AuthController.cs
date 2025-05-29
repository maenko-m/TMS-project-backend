using Microsoft.AspNetCore.Mvc;
using TmsSolution.Application.Dtos.Auth;
using TmsSolution.Application.Interfaces;

namespace TmsSolution.Presentation.Controllers
{
    /// <summary>
    /// Контроллер для обработки операций аутентификации.
    /// </summary>
    [ApiController]
    [Route("api/")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AuthController"/>.
        /// </summary>
        /// <param name="authService">Сервис аутентификации.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Аутентифицирует пользователя и возвращает JWT-токен.
        /// </summary>
        /// <param name="loginRequest">Данные для входа (email и пароль).</param>
        /// <returns>Возвращает <see cref="LoginResponseDto"/> с токеном и временем истечения, либо ошибку 401 при неверных учетных данных.</returns>
        /// <response code="200">Успешная аутентификация, возвращает токен.</response>
        /// <response code="401">Неверный email или пароль.</response>
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
