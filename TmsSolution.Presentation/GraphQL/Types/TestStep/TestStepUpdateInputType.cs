using TmsSolution.Application.Dtos.TestStep;

namespace TmsSolution.Presentation.GraphQL.Types.TestStep
{
    public class TestStepUpdateInputType : InputObjectType<TestStepUpdateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestStepUpdateDto> descriptor)
        {
            descriptor.Name("TestStepUpdateInput");

            descriptor.Field(f => f.TestCaseId).Type<IdType>().Description("The ID of the test case this step belongs to.");
            descriptor.Field(f => f.Description).Type<StringType>().Description("A description of the test step.");
            descriptor.Field(f => f.ExpectedResult).Type<StringType>().Description("Expected result of the step.");
            descriptor.Field(f => f.Position).Type<IntType>().Description("The order or position of the step.");
        }
    }
}
