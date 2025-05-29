using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.Tag;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.GraphQL.Types.TestCase
{
    public class TestCaseUpdateInputType : InputObjectType<TestCaseUpdateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestCaseUpdateDto> descriptor)
        {
            descriptor.Name("TestCaseUpdateInput");

            descriptor.Field(f => f.ProjectId).Type<IdType>().Description("ID of the project.");
            descriptor.Field(f => f.SuiteId).Type<IdType>().Description("ID of the test suite.");
            descriptor.Field(f => f.Title).Type<StringType>().Description("Title of the test case.");
            descriptor.Field(f => f.Description).Type<StringType>().Description("Description of the test case.");
            descriptor.Field(f => f.Preconditions).Type<StringType>().Description("Preconditions for the test case.");
            descriptor.Field(f => f.Postconditions).Type<StringType>().Description("Postconditions of the test case.");
            descriptor.Field(f => f.Status).Type<EnumType<TestCaseStatus>>().Description("Status of the test case.");
            descriptor.Field(f => f.Priority).Type<EnumType<TestCasePriority>>().Description("Priority of the test case.");
            descriptor.Field(f => f.Severity).Type<EnumType<TestCaseSeverity>>().Description("Severity of the test case.");
            descriptor.Field(f => f.CreatedById).Type<IdType>().Description("ID of the creator.");
            descriptor.Field(f => f.Parameters).Type<StringType>().Description("Custom parameters in JSON format.");
            descriptor.Field(f => f.CustomFields).Type<StringType>().Description("Custom fields in JSON format.");
            descriptor.Field(f => f.TagIds).Type<ListType<NonNullType<IdType>>>().Description("List of tag IDs to include in the test case.");
            descriptor.Field(f => f.DefectIds).Type<ListType<NonNullType<IdType>>>().Description("List of defects IDs to include in the test case.");
            descriptor.Field(f => f.StepIds).Type<ListType<NonNullType<IdType>>>().Description("List of test steps IDs to include in the test case.");
        }
    }
}
