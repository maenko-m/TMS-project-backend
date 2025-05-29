using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.Auth
{
    /// <summary>
    /// Модель ответа на запрос аутентификации.
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// JWT-токен для аутентифицированного пользователя.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Время истечения срока действия токена.
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}
