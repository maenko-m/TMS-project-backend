using TmsSolution.Application.Dtos.TestPlan;

namespace TmsSolution.Presentation.GraphQL.Types.TestPlan
{
    public class TestPlanUpdateInputType : InputObjectType<TestPlanUpdateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestPlanUpdateDto> descriptor)
        {
            descriptor.Name("TestPlanUpdateInput").Description("Input type for updating an existing test plan.");

            descriptor.Field(f => f.Name).Type<StringType>().Description("The updated name of the test plan.");
            descriptor.Field(f => f.Description).Type<StringType>().Description("The updated description of the test plan.");
            descriptor.Field(f => f.TestCaseIds).Type<ListType<NonNullType<IdType>>>()
                .Description("The updated list of test case IDs associated with the test plan.");
        }
    }
}
