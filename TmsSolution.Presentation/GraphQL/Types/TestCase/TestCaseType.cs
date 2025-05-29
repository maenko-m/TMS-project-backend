using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.Tag;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Domain.Enums;
using TmsSolution.Presentation.GraphQL.Scalar;

namespace TmsSolution.Presentation.GraphQL.Types.TestCase
{
    public class TestCaseType : ObjectType<TestCaseOutputDto>
    {
        protected override void Configure(IObjectTypeDescriptor<TestCaseOutputDto> descriptor)
        {
            descriptor.Name("TestCase").Description("Represents a test case.");

            descriptor.Field(f => f.Id).Type<NonNullType<IdType>>().Description("The unique identifier of the test case.");
            descriptor.Field(f => f.ProjectId).Type<NonNullType<IdType>>().Description("The ID of the project this test case belongs to.");
            descriptor.Field(f => f.SuiteId).Type<IdType>().Description("The ID of the test suite (optional).");
            descriptor.Field(f => f.Title).Type<NonNullType<StringType>>().Description("The title of the test case.");
            descriptor.Field(f => f.Description).Type<StringType>().Description("A description of the test case.");
            descriptor.Field(f => f.Preconditions).Type<StringType>().Description("Preconditions for executing the test case.");
            descriptor.Field(f => f.Postconditions).Type<StringType>().Description("Postconditions after executing the test case.");
            descriptor.Field(f => f.Status).Type<NonNullType<EnumType<TestCaseStatus>>>().Description("The status of the test case.");
            descriptor.Field(f => f.Priority).Type<NonNullType<EnumType<TestCasePriority>>>().Description("The priority of the test case.");
            descriptor.Field(f => f.Severity).Type<NonNullType<EnumType<TestCaseSeverity>>>().Description("The severity level of the test case.");
            descriptor.Field(f => f.CreatedAt).Type<NonNullType<CustomDateTimeType>>().Description("The creation date of the test case.");
            descriptor.Field(f => f.UpdatedAt).Type<NonNullType<CustomDateTimeType>>().Description("The last update date of the test case.");
            descriptor.Field(f => f.CreatedById).Type<NonNullType<IdType>>().Description("The ID of the user who created the test case.");
            descriptor.Field(f => f.Parameters).Type<StringType>().Description("Custom parameters in JSON format.");
            descriptor.Field(f => f.CustomFields).Type<StringType>().Description("Additional custom fields in JSON format.");
            descriptor.Field(f => f.Tags).Type<ListType<ObjectType<TagOutputDto>>>().Description("Tags associated with the test case.");
            descriptor.Field(f => f.Steps).Type<ListType<ObjectType<TestStepOutputDto>>>().Description("Steps included in the test case.");
            descriptor.Field(f => f.Defects).Type<ListType<ObjectType<DefectOutputDto>>>().Description("Linked defects to the test case.");
        }
    }
}
