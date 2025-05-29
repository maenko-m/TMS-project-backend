namespace TmsSolution.Presentation.GraphQL.Filters
{
    public class MilestoneFilterInput
    {
        public string? Name { get; set; }
        public bool? IsHavingTestRuns { get; set; }
        public bool? IsActual { get; set; }
    }
}
