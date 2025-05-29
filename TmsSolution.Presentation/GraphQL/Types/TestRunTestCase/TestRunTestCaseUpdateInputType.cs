using TmsSolution.Application.Dtos.TestRunTestCase;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.GraphQL.Types.TestRunTestCase
{
    public class TestRunTestCaseUpdateInputType : InputObjectType<TestRunTestCaseUpdateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestRunTestCaseUpdateDto> descriptor)
        {
            descriptor.Name("TestRunTestCaseUpdateInput").Description("Input data to update an existing test run test case.");

            descriptor.Field(f => f.TestRunId).Type<IdType>().Description("Updated test run ID (optional).");
            descriptor.Field(f => f.TestCaseId).Type<IdType>().Description("Updated test case ID (optional).");
            descriptor.Field(f => f.Status).Type<EnumType<TestRunTestCaseStatus>>().Description("Updated status.");
            descriptor.Field(f => f.Comment).Type<StringType>().Description("Updated comment.");
            descriptor.Field(f => f.ExecutionTime).Type<IntType>().Description("Updated execution time in seconds.");
        }
    }
}
