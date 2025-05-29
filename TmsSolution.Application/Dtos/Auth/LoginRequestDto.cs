using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.Auth
{
    /// <summary>
    /// Модель запроса для аутентификации.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
