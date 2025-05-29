using TmsSolution.Application.Dtos.TestRun;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.GraphQL.Types.TestRun
{
    public class TestRunUpdateInputType : InputObjectType<TestRunUpdateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestRunUpdateDto> descriptor)
        {
            descriptor.Name("TestRunUpdateInput").Description("Input data for updating an existing test run.");

            descriptor.Field(f => f.ProjectId).Type<IdType>().Description("Updated project ID (optional).");
            descriptor.Field(f => f.Name).Type<StringType>().Description("Updated name of the test run.");
            descriptor.Field(f => f.Description).Type<StringType>().Description("Updated description.");
            descriptor.Field(f => f.Environment).Type<StringType>().Description("Updated environment.");
            descriptor.Field(f => f.MilestoneId).Type<IdType>().Description("Updated milestone ID.");
            descriptor.Field(f => f.Status).Type<EnumType<TestRunStatus>>().Description("Updated status of the test run.");
            descriptor.Field(f => f.TagIds).Type<ListType<NonNullType<IdType>>>().Description("Updated list of tag IDs.");
            descriptor.Field(f => f.TestRunTestCaseIds).Type<ListType<NonNullType<IdType>>>().Description("Updated list of test case IDs.");
            descriptor.Field(f => f.DefectIds).Type<ListType<NonNullType<IdType>>>().Description("Updated list of defect IDs.");
        }
    }
}
