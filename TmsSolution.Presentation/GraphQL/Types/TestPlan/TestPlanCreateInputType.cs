using TmsSolution.Application.Dtos.TestPlan;

namespace TmsSolution.Presentation.GraphQL.Types.TestPlan
{
    public class TestPlanCreateInputType : InputObjectType<TestPlanCreateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestPlanCreateDto> descriptor)
        {
            descriptor.Name("TestPlanCreateInput").Description("Input type for creating a new test plan.");

            descriptor.Field(f => f.ProjectId).Type<NonNullType<IdType>>().Description("The ID of the project for which the test plan is created.");
            descriptor.Field(f => f.Name).Type<NonNullType<StringType>>().Description("The name of the test plan.");
            descriptor.Field(f => f.Description).Type<StringType>().Description("An optional description for the test plan.");
            descriptor.Field(f => f.TestCaseIds).Type<ListType<NonNullType<IdType>>>()
                .Description("List of test case IDs to include in the test plan.");
        }
    }
}
