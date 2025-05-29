using TmsSolution.Application.Dtos.TestPlan;
using TmsSolution.Presentation.GraphQL.Scalar;
using TmsSolution.Presentation.GraphQL.Types.TestCase;

namespace TmsSolution.Presentation.GraphQL.Types.TestPlan
{
    public class TestPlanType : ObjectType<TestPlanOutputDto>
    {
        protected override void Configure(IObjectTypeDescriptor<TestPlanOutputDto> descriptor)
        {
            descriptor.Name("TestPlan").Description("Represents a test plan containing multiple test cases to validate software quality.");

            descriptor.Field(f => f.Id).Type<NonNullType<IdType>>().Description("The unique identifier of the test plan.");
            descriptor.Field(f => f.ProjectId).Type<NonNullType<IdType>>().Description("The ID of the project associated with this test plan.");
            descriptor.Field(f => f.Name).Type<NonNullType<StringType>>().Description("The name of the test plan.");
            descriptor.Field(f => f.Description).Type<StringType>().Description("An optional description of the test plan.");
            descriptor.Field(f => f.CreatedAt).Type<NonNullType<CustomDateTimeType>>().Description("Date and time when the test plan was created.");
            descriptor.Field(f => f.UpdatedAt).Type<NonNullType<CustomDateTimeType>>().Description("Date and time when the test plan was last updated.");

            descriptor.Field(f => f.TestCases)
                .Type<ListType<NonNullType<TestCaseType>>>()
                .Description("List of test cases associated with this test plan.");
        }
    }
}
