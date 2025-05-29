using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.TestStep
{
    public class TestStepCreateDto
    {
        public Guid TestCaseId { get; set; }

        [Required(ErrorMessage = "Test step description is required.")]
        [StringLength(100, ErrorMessage = "Test step description cannot exceed 100 characters.")]
        public string Description { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Test step expected result cannot exceed 100 characters.")]
        public string? ExpectedResult { get; set; }
        public int Position { get; set; }
    }
}
