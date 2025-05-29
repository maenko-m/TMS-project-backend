using TmsSolution.Domain.Enums;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Types.Project
{
    public class ProjectInvolvementTypeEnum : EnumType<ProjectInvolvement>
    {
        protected override void Configure(IEnumTypeDescriptor<ProjectInvolvement> descriptor)
        {
            descriptor
                .Name("ProjectInvolvement")
                .Description("The type of a project involvement: Owner or Participant");
        }
    }
}
