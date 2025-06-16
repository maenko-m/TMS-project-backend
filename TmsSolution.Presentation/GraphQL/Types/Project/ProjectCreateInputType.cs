using TmsSolution.Application.Dtos.Project;

namespace TmsSolution.Presentation.GraphQL.Types.Project
{
    public class ProjectCreateInputType : InputObjectType<ProjectCreateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<ProjectCreateDto> descriptor)
        {
            descriptor
                .Name("ProjectCreateInput")
                .Description("Input type for creating a project.");

            descriptor
                .Field(p => p.Name)
                .Type<NonNullType<StringType>>()
                .Description("The name of the project (required, max 100 characters).");

            descriptor
                .Field(p => p.Description)
                .Type<StringType>()
                .Description("The description of the project (max 500 characters).");

            descriptor
                .Field(p => p.AccessType)
                .Type<NonNullType<ProjectAccessTypeEnum>>()
                .Description("The access type of the project (Public or Private, required).");

            descriptor
                .Field(p => p.OwnerId)
                .Type<NonNullType<IdType>>()
                .Description("The ID of the project owner (required).");

            descriptor
                .Field(p => p.ProjectUserIds)
                .Type<ListType<NonNullType<IdType>>>()
                .Description("List of user IDs to associate with the project (required for private projects).");

            descriptor
                .Field(p => p.IconPath)
                .Type<StringType>()
                .Description("Path to the project icon file.");
        }
    }
}
