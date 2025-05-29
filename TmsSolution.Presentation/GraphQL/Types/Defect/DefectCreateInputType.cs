using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.GraphQL.Types.Defect
{
    public class DefectCreateInputType : InputObjectType<DefectCreateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<DefectCreateDto> descriptor)
        {
            descriptor.Name("DefectCreateInput").Description("Input for creating a new defect.");

            descriptor.Field(f => f.ProjectId).Type<NonNullType<IdType>>();
            descriptor.Field(f => f.TestRunId).Type<IdType>();
            descriptor.Field(f => f.TestCaseId).Type<IdType>();
            descriptor.Field(f => f.Title).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.ActualResult).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Severity).Type<NonNullType<EnumType<TestCaseSeverity>>>();
            descriptor.Field(f => f.CreatedById).Type<NonNullType<IdType>>();
        }
    }
}
