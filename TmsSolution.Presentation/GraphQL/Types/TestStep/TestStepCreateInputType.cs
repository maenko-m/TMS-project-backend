using TmsSolution.Application.Dtos.TestStep;

namespace TmsSolution.Presentation.GraphQL.Types.TestStep
{
    public class TestStepCreateInputType : InputObjectType<TestStepCreateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestStepCreateDto> descriptor)
        {
            descriptor.Name("TestStepCreateInput");

            descriptor.Field(f => f.TestCaseId).Type<NonNullType<IdType>>().Description("The ID of the test case this step belongs to.");
            descriptor.Field(f => f.Description).Type<NonNullType<StringType>>().Description("A description of the test step.");
            descriptor.Field(f => f.ExpectedResult).Type<StringType>().Description("Expected result of the step.");
            descriptor.Field(f => f.Position).Type<NonNullType<IntType>>().Description("The order or position of the step.");
        }
    }
}
