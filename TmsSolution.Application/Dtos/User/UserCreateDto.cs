using System.ComponentModel.DataAnnotations;
using TmsSolution.Application.Attributes;

namespace TmsSolution.Application.Dtos.User
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "User first name is required.")]
        [StringLength(50, ErrorMessage = "User first name cannot exceed 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "User last name is required.")]
        [StringLength(50, ErrorMessage = "User last name cannot exceed 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "User email is required.")]
        [StringLength(100, ErrorMessage = "User email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email.")]
        public string Email { get; set; } = string.Empty;
        public string? IconPath { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [PasswordComplexity]
        public string Password {  get; set; } = string.Empty;
    }
}
