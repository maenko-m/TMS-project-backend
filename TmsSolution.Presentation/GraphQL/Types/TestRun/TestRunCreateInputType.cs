using TmsSolution.Application.Dtos.TestRun;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.GraphQL.Types.TestRun
{
    public class TestRunCreateInputType : InputObjectType<TestRunCreateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestRunCreateDto> descriptor)
        {
            descriptor.Name("TestRunCreateInput").Description("Input data required to create a new test run.");

            descriptor.Field(f => f.ProjectId).Type<NonNullType<IdType>>().Description("ID of the project where the test run is created.");
            descriptor.Field(f => f.Name).Type<NonNullType<StringType>>().Description("Name of the test run.");
            descriptor.Field(f => f.Description).Type<StringType>().Description("Optional description of the test run.");
            descriptor.Field(f => f.Environment).Type<StringType>().Description("Environment in which the test run will be executed.");
            descriptor.Field(f => f.MilestoneId).Type<IdType>().Description("Optional milestone linked to this test run.");
            descriptor.Field(f => f.Status).Type<NonNullType<EnumType<TestRunStatus>>>().Description("Initial status of the test run.");
            descriptor.Field(f => f.TagIds).Type<ListType<NonNullType<IdType>>>().Description("List of tag IDs to associate with the test run.");
            descriptor.Field(f => f.TestRunTestCaseIds).Type<ListType<NonNullType<IdType>>>().Description("List of test case IDs to include in the test run.");
            descriptor.Field(f => f.DefectIds).Type<ListType<NonNullType<IdType>>>().Description("List of defect IDs linked to the test run.");
        }
    }
}
