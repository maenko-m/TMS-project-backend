using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmsSolution.Domain.Enums;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class Defect : IAuditable
    {
        [Key] 
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public Guid? TestRunId { get; set; }

        [ForeignKey("TestRunId")]
        public TestRun? TestRun { get; set; }

        public Guid? TestCaseId { get; set; }

        [ForeignKey("TestCaseId")]
        public TestCase? TestCase { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string ActualResult { get; set; } = string.Empty;

        public TestCaseSeverity Severity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }

        public string? CustomFields { get; set; } // JSON


        public List<Attachment> Attachments { get; set; } = new();
    }
}
