using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Application.Dtos.Tag;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.Attachment;

namespace TmsSolution.Application.Dtos.TestCase
{
    public class TestCaseOutputDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? SuiteId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Preconditions { get; set; }
        public string? Postconditions { get; set; }
        public TestCaseStatus Status { get; set; }
        public TestCasePriority Priority { get; set; }
        public TestCaseSeverity Severity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedById { get; set; }
        public string? Parameters { get; set; } // JSON
        public string? CustomFields { get; set; } // JSON

        //public List<AttachmentOutputDto> Attachments { get; set; } = new();
        public List<TagOutputDto> Tags { get; set; } = new();
        public List<TestStepOutputDto> Steps { get; set; } = new();
        public List<DefectOutputDto> Defects { get; set; } = new();
    }
}
