using TmsSolution.Application.Dtos.TestStep;

namespace TmsSolution.Presentation.GraphQL.Types.TestStep
{
    public class TestStepType : ObjectType<TestStepOutputDto>
    {
        protected override void Configure(IObjectTypeDescriptor<TestStepOutputDto> descriptor)
        {
            descriptor.Name("TestStep").Description("Represents a step in a test case.");

            descriptor.Field(f => f.Id).Type<NonNullType<IdType>>().Description("The unique identifier of the test step.");
            descriptor.Field(f => f.TestCaseId).Type<NonNullType<IdType>>().Description("The ID of the test case this step belongs to.");
            descriptor.Field(f => f.Description).Type<NonNullType<StringType>>().Description("A description of the test step.");
            descriptor.Field(f => f.ExpectedResult).Type<StringType>().Description("Expected result of the step.");
            descriptor.Field(f => f.Position).Type<NonNullType<IntType>>().Description("The order or position of the step.");
            descriptor.Field(f => f.CreatedAt).Type<NonNullType<DateTimeType>>().Description("Date and time when the step was created.");
            descriptor.Field(f => f.UpdatedAt).Type<NonNullType<DateTimeType>>().Description("Date and time when the step was last updated.");
        }
    }
}
