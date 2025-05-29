using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmsSolution.Domain.Enums;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class TestRunTestCase : IAuditable
    {
        [Key] 
        public Guid Id { get; set; }

        public Guid TestRunId { get; set; }

        [ForeignKey("TestRunId")]
        public TestRun TestRun { get; set; }

        public Guid TestCaseId { get; set; }

        [ForeignKey("TestCaseId")]
        public TestCase TestCase { get; set; }

        [Required]
        public TestRunTestCaseStatus Status { get; set; }

        public string? Comment { get; set; }

        public int ExecutionTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
