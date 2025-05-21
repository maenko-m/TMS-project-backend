using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Domain.Entities
{
    public class TestPlan : IAuditable
    {
        [Key] 
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Guid CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }

        public List<TestPlanTestCase> TestPlanTestCases { get; set; } = new();
    }
}
