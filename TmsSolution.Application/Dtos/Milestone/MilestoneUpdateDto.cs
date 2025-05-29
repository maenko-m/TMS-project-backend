using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Application.Dtos.Milestone
{
    public class MilestoneUpdateDto
    {
        public Guid? ProjectId { get; set; }

        [StringLength(255, ErrorMessage = "Milestone name cannot exceed 255 characters.")]
        public string? Name { get; set; }

        [StringLength(255, ErrorMessage = "Milestone description cannot exceed 255 characters.")]
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
