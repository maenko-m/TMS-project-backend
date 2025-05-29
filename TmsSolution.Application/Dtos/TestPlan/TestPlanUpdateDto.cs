using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.TestPlan
{
    public class TestPlanUpdateDto
    {
        public Guid? ProjectId { get; set; }

        [StringLength(255, ErrorMessage = "Test plan name cannot exceed 255 characters.")]
        public string? Name { get; set; }

        [StringLength(255, ErrorMessage = "Test plan name cannot exceed 255 characters.")]
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? CreatedById { get; set; }

        public List<Guid>? TestCaseIds { get; set; } = new();
    }
}
