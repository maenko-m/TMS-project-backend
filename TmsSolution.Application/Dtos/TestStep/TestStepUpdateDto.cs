using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.TestStep
{
    public class TestStepUpdateDto
    {
        public Guid? TestCaseId { get; set; }

        [StringLength(100, ErrorMessage = "Test step description cannot exceed 100 characters.")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Test step expected result cannot exceed 100 characters.")]
        public string? ExpectedResult { get; set; }
        public int? Position { get; set; }
    }
}
