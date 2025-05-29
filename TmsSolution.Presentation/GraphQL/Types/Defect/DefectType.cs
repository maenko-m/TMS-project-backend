using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.GraphQL.Types.Defect
{
    public class DefectType : ObjectType<DefectOutputDto>
    {
        protected override void Configure(IObjectTypeDescriptor<DefectOutputDto> descriptor)
        {
            descriptor.Name("Defect").Description("Represents a defect found in the system.");

            descriptor.Field(f => f.Id).Type<NonNullType<IdType>>().Description("The unique identifier of the defect.");
            descriptor.Field(f => f.ProjectId).Type<NonNullType<IdType>>().Description("The ID of the project to which this defect belongs.");
            descriptor.Field(f => f.TestRunId).Type<IdType>().Description("Optional ID of the related test run.");
            descriptor.Field(f => f.TestCaseId).Type<IdType>().Description("Optional ID of the related test case.");
            descriptor.Field(f => f.Title).Type<NonNullType<StringType>>().Description("Title of the defect.");
            descriptor.Field(f => f.ActualResult).Type<NonNullType<StringType>>().Description("The actual result observed that led to the defect.");
            descriptor.Field(f => f.Severity).Type<NonNullType<EnumType<TestCaseSeverity>>>().Description("Severity level of the defect.");
            descriptor.Field(f => f.CreatedAt).Type<NonNullType<DateTimeType>>().Description("Timestamp when the defect was created.");
            descriptor.Field(f => f.UpdatedAt).Type<NonNullType<DateTimeType>>().Description("Timestamp when the defect was last updated.");
            descriptor.Field(f => f.CreatedById).Type<NonNullType<IdType>>().Description("ID of the user who created the defect.");
        }
    }
}
