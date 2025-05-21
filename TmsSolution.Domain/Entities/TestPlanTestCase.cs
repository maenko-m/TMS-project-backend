using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TmsSolution.Domain.Entities
{
    public class TestPlanTestCase
    {
        [Key] 
        public Guid Id { get; set; }

        public Guid TestPlanId { get; set; }

        [ForeignKey("TestPlanId")]
        public TestPlan TestPlan { get; set; }

        public Guid TestCaseId { get; set; }

        [ForeignKey("TestCaseId")]
        public TestCase TestCase { get; set; }
    }
}
