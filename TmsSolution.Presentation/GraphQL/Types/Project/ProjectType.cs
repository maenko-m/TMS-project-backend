using TmsSolution.Application.Dtos.Project;
using TmsSolution.Presentation.GraphQL.Scalar;

namespace TmsSolution.Presentation.GraphQL.Types.Project
{
    public class ProjectType : ObjectType<ProjectOutputDto>
    {
        protected override void Configure(IObjectTypeDescriptor<ProjectOutputDto> descriptor)
        {
            descriptor
                .Name("Project")
                .Description("A project entity with details and statistics.");

            descriptor
                .Field(p => p.Id)
                .Type<NonNullType<IdType>>()
                .Description("The unique identifier of the project.");

            descriptor
                .Field(p => p.Name)
                .Type<NonNullType<StringType>>()
                .Description("The name of the project.");

            descriptor
                .Field(p => p.Description)
                .Type<StringType>()
                .Description("The description of the project.");

            descriptor
                .Field(p => p.IconBase64)
                .Type<StringType>()
                .Description("The Base64-encoded icon of the project.");

            descriptor
                .Field(p => p.AccessType)
                .Type<NonNullType<ProjectAccessTypeEnum>>()
                .Description("The access type of the project (Public or Private).");

            descriptor
                .Field(p => p.CreatedAt)
                .Type<NonNullType<CustomDateTimeType>>()
                .Description("The creation date of the project.");

            descriptor
                .Field(p => p.UpdatedAt)
                .Type<NonNullType<CustomDateTimeType>>()
                .Description("The last update date of the project.");

            descriptor
                .Field(p => p.OwnerId)
                .Type<NonNullType<IdType>>()
                .Description("The ID of the project owner.");

            descriptor
                .Field(p => p.OwnerFullName)
                .Type<NonNullType<StringType>>()
                .Description("The full name of the project owner.");

            descriptor
                .Field(p => p.ProjectUsersCount)
                .Type<NonNullType<IntType>>()
                .Description("The number of users associated with the project.");

            descriptor
                .Field(p => p.TestCasesCount)
                .Type<NonNullType<IntType>>()
                .Description("The number of test cases in the project.");

            descriptor
                .Field(p => p.DefectsCount)
                .Type<NonNullType<IntType>>()
                .Description("The number of defects in the project.");
        }
    }
}
