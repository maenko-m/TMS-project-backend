using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.TestSuite
{
    public class TestSuiteCreateDto
    {
        [Required(ErrorMessage = "Project id is required.")]
        public Guid ProjectId { get; set; }

        [Required(ErrorMessage = "Test suit name is required.")]
        [StringLength(255, ErrorMessage = "Test suit name cannot exceed 255 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Test suit description cannot exceed 255 characters.")]
        public string? Description { get; set; }

        [StringLength(255, ErrorMessage = "Test suit preconditions cannot exceed 255 characters.")]
        public string? Preconditions { get; set; }
    }
}
