using TmsSolution.Application.Dtos.Project;

namespace TmsSolution.Presentation.GraphQL.Types.Project
{
    public class ProjectUpdateInputType : InputObjectType<ProjectUpdateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<ProjectUpdateDto> descriptor)
        {
            descriptor
                .Name("ProjectUpdateInput")
                .Description("Input type for updating a project.");

            descriptor
                .Field(p => p.Name)
                .Type<StringType>()
                .Description("The name of the project (optional, max 100 characters).");

            descriptor
                .Field(p => p.Description)
                .Type<StringType>()
                .Description("The description of the project (optional, max 500 characters).");

            descriptor
                .Field(p => p.AccessType)
                .Type<ProjectAccessTypeEnum>()
                .Description("The access type of the project (Public or Private, optional).");

            descriptor
                .Field(p => p.OwnerId)
                .Type<IdType>()
                .Description("The ID of the project owner (optional).");

            descriptor
                .Field(p => p.ProjectUserIds)
                .Type<ListType<NonNullType<IntType>>>()
                .Description("List of user IDs to associate with the project (optional).");

            descriptor
                .Field(p => p.IconPath)
                .Type<StringType>()
                .Description("Path to the project icon file (optional, max 500 characters).");
        }
    }
}
