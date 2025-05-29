using TmsSolution.Application.Dtos.TestRunTestCase;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.GraphQL.Types.TestRunTestCase
{
    public class TestRunTestCaseCreateInputType : InputObjectType<TestRunTestCaseCreateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestRunTestCaseCreateDto> descriptor)
        {
            descriptor.Name("TestRunTestCaseCreateInput").Description("Input data to add a new test case to a test run.");

            descriptor.Field(f => f.TestRunId).Type<NonNullType<IdType>>().Description("ID of the test run to add the case to.");
            descriptor.Field(f => f.TestCaseId).Type<NonNullType<IdType>>().Description("ID of the test case being added.");
            descriptor.Field(f => f.Status).Type<NonNullType<EnumType<TestRunTestCaseStatus>>>().Description("Execution status for this test case.");
            descriptor.Field(f => f.Comment).Type<StringType>().Description("Optional comment for this test run test case.");
            descriptor.Field(f => f.ExecutionTime).Type<NonNullType<IntType>>().Description("Execution time in seconds.");
        }
    }
}
