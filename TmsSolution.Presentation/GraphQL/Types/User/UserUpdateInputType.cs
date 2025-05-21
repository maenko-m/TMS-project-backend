using TmsSolution.Application.Dtos.User;

namespace TmsSolution.Presentation.GraphQL.Types.User
{
    public class UserUpdateInputType : InputObjectType<UserUpdateDto>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UserUpdateDto> descriptor)
        {
            descriptor
                .Name("UserUpdateInput")
                .Description("Input type for updating user information.");

            descriptor
                .Field(u => u.FirstName)
                .Type<StringType>()
                .Description("The updated first name of the user (optional, max 50 characters).");

            descriptor
                .Field(u => u.LastName)
                .Type<StringType>()
                .Description("The updated last name of the user (optional, max 50 characters).");

            descriptor
                .Field(u => u.Email)
                .Type<StringType>()
                .Description("The updated email address of the user (optional, max 100 characters).");

            descriptor
                .Field(u => u.Password)
                .Type<StringType>()
                .Description("The new password for the user account (optional, must meet complexity requirements).");

            descriptor
                .Field(u => u.IconPath)
                .Type<StringType>()
                .Description("The updated path to the user profile icon (optional).");
        }
    }
}
