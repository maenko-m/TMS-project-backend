using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TmsSolution.Application.Attributes
{
    public class JsonValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var str = value as string;

            if (string.IsNullOrWhiteSpace(str))
                return ValidationResult.Success;

            try
            {
                JsonDocument.Parse(str);
                return ValidationResult.Success;
            }
            catch (JsonException)
            {
                return new ValidationResult("Invalid JSON format.");
            }
        }
    }
}
