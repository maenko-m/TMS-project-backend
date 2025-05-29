namespace TmsSolution.Presentation.GraphQL.Filters
{
    public class TestCaseFilterInput
    {
        public string? Title { get; set; }
        public Guid? SuiteId { get; set; }
        public bool? IsHavingDefects { get; set; }
        public List<Guid>? TagIds { get; set; }
    }
}
