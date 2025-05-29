using TmsSolution.Application.Dtos.TestSuite;

namespace TmsSolution.Presentation.GraphQL.Types.TestSuite
{
    public class TestSuiteType : ObjectType<TestSuiteOutputDto>
    {
        protected override void Configure(IObjectTypeDescriptor<TestSuiteOutputDto> descriptor)
        {
            descriptor
                .Name("TestSuite")
                .Description("Represents a test suite within a project.");

            descriptor
                .Field(t => t.Id)
                .Type<NonNullType<IdType>>()
                .Description("The unique identifier of the test suite.");

            descriptor
                .Field(t => t.ProjectId)
                .Type<NonNullType<IdType>>()
                .Description("The identifier of the project this test suite belongs to.");

            descriptor
                .Field(t => t.Name)
                .Type<NonNullType<StringType>>()
                .Description("The name of the test suite.");

            descriptor
                .Field(t => t.Description)
                .Type<StringType>()
                .Description("The optional description of the test suite.");

            descriptor
                .Field(t => t.Preconditions)
                .Type<StringType>()
                .Description("The optional preconditions for the test suite.");

            descriptor
                .Field(t => t.CreatedAt)
                .Type<NonNullType<DateTimeType>>()
                .Description("The date and time when the test suite was created.");

            descriptor
                .Field(t => t.UpdatedAt)
                .Type<NonNullType<DateTimeType>>()
                .Description("The date and time when the test suite was last updated.");

            descriptor
                .Field(t => t.TestCasesCount)
                .Type<NonNullType<IntType>>()
                .Description("The number of test cases in this test suite.");
        }
    }
}
