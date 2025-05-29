using TmsSolution.Application.Dtos.User;
using TmsSolution.Domain.Enums;
using TmsSolution.Presentation.GraphQL.Scalar;
using TmsSolution.Presentation.GraphQL.Types.Project;

namespace TmsSolution.Presentation.GraphQL.Types.User
{
    public class UserType : ObjectType<UserOutputDto>
    {
        protected override void Configure(IObjectTypeDescriptor<UserOutputDto> descriptor)
        {
            descriptor
                .Name("User")
                .Description("Represents a user of the system.");

            descriptor
                .Field(u => u.Id)
                .Type<NonNullType<IdType>>()
                .Description("The unique identifier of the user.");

            descriptor
                .Field(u => u.FullName)
                .Type<NonNullType<StringType>>()
                .Description("The full name of the user (first and last name combined).");

            descriptor
                .Field(u => u.Email)
                .Type<NonNullType<StringType>>()
                .Description("The email address of the user.");

            descriptor
                .Field(u => u.IconBase64)
                .Type<StringType>()
                .Description("The Base64-encoded profile icon of the user.");

            descriptor
                .Field(u => u.Role)
                .Type<NonNullType<EnumType<UserRole>>>()
                .Description("The role of the user in the system.");

            descriptor
                .Field(u => u.CreatedAt)
                .Type<NonNullType<CustomDateTimeType>>()
                .Description("The date and time the user was created.");

            descriptor
                .Field(u => u.UpdatedAt)
                .Type<NonNullType<CustomDateTimeType>>()
                .Description("The date and time the user was last updated.");
        }
    }
}
