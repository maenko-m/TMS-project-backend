using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class TestSuite : IAuditable
    {
        [Key] 
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public Guid? ParentSuiteId { get; set; }

        [ForeignKey("ParentSuiteId")]
        public TestSuite? ParentSuite { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Preconditions { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<TestCase> TestCases { get; set; } = new();
        public List<TestSuite> ChildSuites { get; set; } = new();
    }
}
