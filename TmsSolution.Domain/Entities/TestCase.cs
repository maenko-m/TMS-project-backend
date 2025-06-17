using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmsSolution.Domain.Enums;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class TestCase : IAuditable
    {
        [Key] 
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public Guid? SuiteId { get; set; }

        [ForeignKey("SuiteId")]
        public TestSuite? Suite { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? Preconditions { get; set; }
        public string? Postconditions { get; set; }

        [Required]
        public TestCaseStatus Status { get; set; }

        public TestCasePriority Priority { get; set; }

        public TestCaseSeverity Severity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }

        public string? Parameters { get; set; } // JSON

        public string? CustomFields { get; set; } // JSON

        public List<Tag> Tags { get; set; } = new();
        public List<TestStep> Steps { get; set; } = new();
        public List<TestRunTestCase> TestRunTestCases { get; set; } = new();
        public List<Defect> Defects { get; set; } = new();
        public List<TestPlanTestCase> TestPlanTestCases { get; set; } = new();
        public List<Attachment> Attachments { get; set; } = new();
    }
}
