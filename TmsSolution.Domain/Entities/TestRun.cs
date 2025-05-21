using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmsSolution.Domain.Enums;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class TestRun : IAuditable
    {
        [Key] 
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Environment { get; set; }

        public Guid? MilestoneId { get; set; }

        [ForeignKey("MilestoneId")]
        public Milestone? Milestone { get; set; }

        [Required]
        public TestRunStatus Status { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid? AssignedToId { get; set; }

        [ForeignKey("AssignedToId")]
        public User? AssignedTo { get; set; }

        public string? CustomFields { get; set; } // JSON
        public string? ExternalIssueId { get; set; }

        public List<Tag> Tags { get; set; } = new();
        public List<TestRunTestCase> TestRunTestCases { get; set; } = new();
        public List<Defect> Defects { get; set; } = new();
    }
}
