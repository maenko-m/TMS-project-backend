using TmsSolution.Domain.Enums;

namespace TmsSolution.Presentation.GraphQL.Types.Project
{
    public class ProjectAccessTypeEnum : EnumType<ProjectAccessType>
    {
        protected override void Configure(IEnumTypeDescriptor<ProjectAccessType> descriptor)
        {
            descriptor
                .Name("ProjectAccessType")
                .Description("The access type of a project (Public or Private).");
        }
    }
}
