using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Types.TestSuite
{
    public class TestSuiteFilterInputType : InputObjectType<TestSuiteFilterInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestSuiteFilterInput> descriptor)
        {
            descriptor.Name("TestSuiteFilter");

            descriptor.Field("name")
                .Type<StringType>()
                .Description("Filter test suites by substring in the name");
        }
    }
}
