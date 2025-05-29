using TmsSolution.Application.Dtos.Tag;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.GraphQL.Types.TestCase
{
    public class TestCaseCreateInputType : InputObjectType<TestCaseCreateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TestCaseCreateDto> descriptor)
        {
            descriptor.Name("TestCaseCreateInput");

            descriptor.Field(f => f.ProjectId).Type<NonNullType<IdType>>().Description("The ID of the project.");
            descriptor.Field(f => f.SuiteId).Type<IdType>().Description("The ID of the test suite.");
            descriptor.Field(f => f.Title).Type<NonNullType<StringType>>().Description("The title of the test case.");
            descriptor.Field(f => f.Description).Type<StringType>().Description("A description of the test case.");
            descriptor.Field(f => f.Preconditions).Type<StringType>().Description("Preconditions for the test case.");
            descriptor.Field(f => f.Postconditions).Type<StringType>().Description("Postconditions for the test case.");
            descriptor.Field(f => f.Status).Type<NonNullType<EnumType<TestCaseStatus>>>().Description("Status of the test case.");
            descriptor.Field(f => f.Priority).Type<NonNullType<EnumType<TestCasePriority>>>().Description("Priority of the test case.");
            descriptor.Field(f => f.Severity).Type<NonNullType<EnumType<TestCaseSeverity>>>().Description("Severity of the test case.");
            descriptor.Field(f => f.CreatedById).Type<NonNullType<IdType>>().Description("ID of the user who created the test case.");
            descriptor.Field(f => f.Parameters).Type<StringType>().Description("Custom parameters in JSON.");
            descriptor.Field(f => f.CustomFields).Type<StringType>().Description("Custom fields in JSON.");
            descriptor.Field(f => f.TagIds).Type<ListType<NonNullType<IdType>>>().Description("List of tag IDs to include in the test case.");
            descriptor.Field(f => f.DefectIds).Type<ListType<NonNullType<IdType>>>().Description("List of defects IDs to include in the test case.");
            descriptor.Field(f => f.StepIds).Type<ListType<NonNullType<IdType>>>().Description("List of test steps IDs to include in the test case.");
        }
    }
}
