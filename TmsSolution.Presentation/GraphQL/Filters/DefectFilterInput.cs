using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.GraphQL.Filters
{
    public class DefectFilterInput
    {
        public string? Title { get; set; }
        public Guid? TestRunId { get; set; }
        public Guid? TestCaseId { get; set; }
        public TestCaseSeverity? Severity { get; set; }
    }
}
