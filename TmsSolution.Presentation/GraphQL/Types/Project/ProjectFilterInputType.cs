using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Types.Project
{
    public class ProjectFilterInputType : InputObjectType<ProjectFilterInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<ProjectFilterInput> descriptor)
        {
            descriptor.Name("ProjectFilter");

            descriptor.Field("name")
                .Type<StringType>()
                .Description("Filter projects by substring in the name");

            descriptor.Field("involvement")
                .Type<EnumType<ProjectInvolvement>>()
                .Description("Filter projects by involvement: Owner or Participant");
        }
    }
}
