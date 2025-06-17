using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.TestCase;

namespace TmsSolution.Application.Dtos.TestPlan
{
    public class TestPlanCreateDto
    {
        public Guid ProjectId { get; set; }

        [Required(ErrorMessage = "Test plan name is required.")]
        [StringLength(255, ErrorMessage = "Test plan name cannot exceed 255 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Test plan name cannot exceed 255 characters.")]
        public string? Description { get; set; }
        public Guid CreatedById { get; set; }

        public List<Guid>? TestCaseIds { get; set; } = new();
    }
}
