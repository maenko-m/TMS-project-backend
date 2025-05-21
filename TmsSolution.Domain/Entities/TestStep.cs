using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class TestStep : IAuditable
    {
        [Key] 
        public Guid Id { get; set; }

        public Guid TestCaseId { get; set; }

        [ForeignKey("TestCaseId")]
        public TestCase TestCase { get; set; }

        public Guid? ParentStepId { get; set; }

        [ForeignKey("ParentStepId")]
        public TestStep? ParentStep { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? ExpectedResult { get; set; }

        public int Position { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string? Attachments { get; set; } // JSON 

        public List<TestStep> ChildSteps { get; set; } = new();
    }
}
