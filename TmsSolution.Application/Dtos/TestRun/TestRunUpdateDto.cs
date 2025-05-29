using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Application.Dtos.TestRun
{
    public class TestRunUpdateDto
    {
        public Guid? ProjectId { get; set; }

        [StringLength(255, ErrorMessage = "Test run name cannot exceed 255 characters.")]
        public string? Name { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Test run description cannot exceed 255 characters.")]
        public string? Description { get; set; }

        [StringLength(255, ErrorMessage = "Test run environment cannot exceed 255 characters.")]
        public string? Environment { get; set; }
        public Guid? MilestoneId { get; set; }
        public TestRunStatus? Status { get; set; }

        public List<Guid>? TagIds { get; set; } = new();
        public List<Guid>? TestRunTestCaseIds { get; set; } = new();
        public List<Guid>? DefectIds { get; set; } = new();
    }
}
