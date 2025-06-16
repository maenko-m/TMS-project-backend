using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.GraphQL.Types.Defect
{
    public class DefectUpdateInputType : InputObjectType<DefectUpdateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<DefectUpdateDto> descriptor)
        {
            descriptor.Name("DefectUpdateInput").Description("Input for updating an existing defect.");

            descriptor.Field(f => f.ProjectId).Type<IdType>();
            descriptor.Field(f => f.TestRunId).Type<IdType>();
            descriptor.Field(f => f.TestCaseId).Type<IdType>();
            descriptor.Field(f => f.Title).Type<StringType>();
            descriptor.Field(f => f.ActualResult).Type<StringType>();
            descriptor.Field(f => f.Severity).Type<EnumType<TestCaseSeverity>>();
            descriptor.Field(f => f.CreatedById).Type<IdType>();
        }
    }
}
