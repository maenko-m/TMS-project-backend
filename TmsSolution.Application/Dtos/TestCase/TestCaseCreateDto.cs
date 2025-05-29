using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Attributes;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.Tag;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Application.Dtos.TestCase
{
    public class TestCaseCreateDto
    {
        public Guid ProjectId { get; set; }
        public Guid? SuiteId { get; set; }

        [Required(ErrorMessage = "Test case title is required.")]
        [StringLength(255, ErrorMessage = "Test case title cannot exceed 255 characters.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Test case title cannot exceed 255 characters.")]
        public string? Description { get; set; }

        [StringLength(255, ErrorMessage = "Test case title cannot exceed 255 characters.")]
        public string? Preconditions { get; set; }

        [StringLength(255, ErrorMessage = "Test case title cannot exceed 255 characters.")]
        public string? Postconditions { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(TestCaseStatus), ErrorMessage = "Invalid status type.")]
        public TestCaseStatus Status { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        [EnumDataType(typeof(TestCasePriority), ErrorMessage = "Invalid priority type.")]
        public TestCasePriority Priority { get; set; }

        [Required(ErrorMessage = "Severity is required.")]
        [EnumDataType(typeof(TestCaseSeverity), ErrorMessage = "Invalid severity type.")]
        public TestCaseSeverity Severity { get; set; }
        public Guid CreatedById { get; set; }

        [JsonValidation]
        public string? Parameters { get; set; } // JSON

        [JsonValidation]
        public string? CustomFields { get; set; } // JSON
    }
}
