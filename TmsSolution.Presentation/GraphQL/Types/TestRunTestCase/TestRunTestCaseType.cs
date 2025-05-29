using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Dtos.TestRunTestCase;
using TmsSolution.Domain.Enums;
using TmsSolution.Presentation.GraphQL.Scalar;

namespace TmsSolution.Presentation.GraphQL.Types.TestRunTestCase
{
    public class TestRunTestCaseType : ObjectType<TestRunTestCaseOutputDto>
    {
        protected override void Configure(IObjectTypeDescriptor<TestRunTestCaseOutputDto> descriptor)
        {
            descriptor.Name("TestRunTestCase").Description("Represents a test case inside a test run.");

            descriptor.Field(f => f.Id).Type<NonNullType<IdType>>().Description("Unique identifier.");
            descriptor.Field(f => f.TestRunId).Type<NonNullType<IdType>>().Description("ID of the test run.");
            descriptor.Field(f => f.TestCase).Type<NonNullType<ObjectType<TestCaseOutputDto>>>().Description("Related test case.");
            descriptor.Field(f => f.Status).Type<NonNullType<EnumType<TestRunTestCaseStatus>>>().Description("Execution status.");
            descriptor.Field(f => f.Comment).Type<StringType>().Description("Comment related to the execution.");
            descriptor.Field(f => f.ExecutionTime).Type<NonNullType<IntType>>().Description("Execution duration in seconds.");
            descriptor.Field(f => f.CreatedAt).Type<NonNullType<CustomDateTimeType>>().Description("Creation timestamp.");
            descriptor.Field(f => f.UpdatedAt).Type<NonNullType<CustomDateTimeType>>().Description("Last update timestamp.");
        }
    }
}
