using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Utilities
{
    public static class Validator
    {
        public static void Validate<T>(T obj, Func<T, IEnumerable<ValidationResult>>? customValidation = null) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj);

            bool isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(obj, context, results, true);

            if (customValidation != null)
            {
                results.AddRange(customValidation(obj));
                isValid &= !results.Any(r => r != ValidationResult.Success);
            }

            if (!isValid)
            {
                var errors = string.Join("; ", results.Select(r => r.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }
        }
    }
}
