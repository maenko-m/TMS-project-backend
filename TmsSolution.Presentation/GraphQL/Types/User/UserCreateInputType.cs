using TmsSolution.Application.Dtos.User;

namespace TmsSolution.Presentation.GraphQL.Types.User
{
    public class UserCreateInputType : InputObjectType<UserCreateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UserCreateDto> descriptor)
        {
            descriptor
                .Name("UserCreateInput")
                .Description("Input type for creating a new user.");

            descriptor
                .Field(u => u.FirstName)
                .Type<NonNullType<StringType>>()
                .Description("The first name of the user (required, max 50 characters).");

            descriptor
                .Field(u => u.LastName)
                .Type<NonNullType<StringType>>()
                .Description("The last name of the user (required, max 50 characters).");

            descriptor
                .Field(u => u.Email)
                .Type<NonNullType<StringType>>()
                .Description("The email address of the user (required, max 100 characters).");

            descriptor
                .Field(u => u.Password)
                .Type<NonNullType<StringType>>()
                .Description("The password for the user account (required, must meet complexity requirements).");

            descriptor
                .Field(u => u.IconPath)
                .Type<StringType>()
                .Description("The path to the user profile icon (optional).");
        }
    }
}
