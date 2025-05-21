using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TmsSolution.Application.Attributes
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        public PasswordComplexityAttribute()
        {
            ErrorMessage = "Password must be at least 13 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.";
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true; 

            var password = value as string;

            if (string.IsNullOrWhiteSpace(password))
                return false; 

            if (password.Length < 13)
                return false;

            var hasUpper = Regex.IsMatch(password, @"[A-Z]");
            var hasLower = Regex.IsMatch(password, @"[a-z]");
            var hasDigit = Regex.IsMatch(password, @"\d");
            var hasSpecial = Regex.IsMatch(password, @"[\W_]");

            return hasUpper && hasLower && hasDigit && hasSpecial;
        }
    }
}
