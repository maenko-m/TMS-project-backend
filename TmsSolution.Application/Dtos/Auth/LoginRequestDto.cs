using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.Auth
{
    /// <summary>
    /// Data Transfer Object for user login credentials.
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// The user's email address used for login.
        /// </summary>
        /// <remarks>This field is required. Must be a valid email format.</remarks>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The user's password used for login.
        /// </summary>
        /// <remarks>This field is required.</remarks>
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;
    }
}
