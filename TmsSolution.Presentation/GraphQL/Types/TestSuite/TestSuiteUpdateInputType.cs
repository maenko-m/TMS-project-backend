using TmsSolution.Application.Dtos.TestSuite;

namespace TmsSolution.Presentation.GraphQL.Types.TestSuite
{
    public class TestSuiteUpdateInputType : InputObjectType<TestSuiteUpdateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestSuiteUpdateDto> descriptor)
        {
            descriptor
                .Name("TestSuiteUpdateInput")
                .Description("Input type for updating a test suite.");

            descriptor.Field(t => t.ProjectId)
                .Type<IdType>()
                .Description("Project ID of the test suite.");

            descriptor.Field(t => t.Name)
                .Type<StringType>()
                .Description("Name of the test suite.");

            descriptor.Field(t => t.Description)
                .Type<StringType>()
                .Description("Optional description.");

            descriptor.Field(t => t.Preconditions)
                .Type<StringType>()
                .Description("Optional preconditions.");
        }
    }
}
