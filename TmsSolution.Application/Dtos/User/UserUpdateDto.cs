using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Attributes;

namespace TmsSolution.Application.Dtos.User
{
    public class UserUpdateDto
    {
        [StringLength(50, ErrorMessage = "User first name cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "User last name cannot exceed 50 characters.")]
        public string? LastName { get; set; }

        [StringLength(100, ErrorMessage = "User email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email.")]
        public string? Email { get; set; }
        public string? IconPath { get; set; }

        [PasswordComplexity]
        public string? Password { get; set; }
    }
}
