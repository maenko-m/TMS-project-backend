using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Application.Dtos.Defect
{
    public class DefectUpdateDto
    {
        public Guid? ProjectId { get; set; }
        public Guid? TestRunId { get; set; }
        public Guid? TestCaseId { get; set; }

        [StringLength(255, ErrorMessage = "Defect title cannot exceed 255 characters.")]
        public string? Title { get; set; }

        [StringLength(255, ErrorMessage = "Defect actual result cannot exceed 255 characters.")]
        public string? ActualResult { get; set; }

        [EnumDataType(typeof(TestCaseSeverity), ErrorMessage = "Invalid severity type.")]
        public TestCaseSeverity? Severity { get; set; }
        public Guid? CreatedById { get; set; }
    }
}
