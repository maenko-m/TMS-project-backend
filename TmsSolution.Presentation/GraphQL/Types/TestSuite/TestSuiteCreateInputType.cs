using TmsSolution.Application.Dtos.TestSuite;

namespace TmsSolution.Presentation.GraphQL.Types.TestSuite
{
    public class TestSuiteCreateInputType : InputObjectType<TestSuiteCreateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestSuiteCreateDto> descriptor)
        {
            descriptor
                .Name("TestSuiteCreateInput")
                .Description("Input type for creating a test suite.");

            descriptor.Field(t => t.ProjectId)
                .Type<NonNullType<IdType>>()
                .Description("Project ID for the new test suite.");

            descriptor.Field(t => t.Name)
                .Type<NonNullType<StringType>>()
                .Description("Name of the new test suite.");

            descriptor.Field(t => t.Description)
                .Type<StringType>()
                .Description("Optional description.");

            descriptor.Field(t => t.Preconditions)
                .Type<StringType>()
                .Description("Optional preconditions.");
        }
    }
}
