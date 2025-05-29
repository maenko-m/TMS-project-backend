using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.GraphQL.Filters
{
    public class TestRunFilterInput
    {
        public string? Name { get; set; }
        public Guid? MilestoneId { get; set; }
        public TestRunStatus? Status { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
