using TmsSolution.Application.Dtos.User;
using TmsSolution.Application.Interfaces;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class UserMutation
    {
        public async Task<bool> CreateUser(
            UserCreateDto input,
            [Service] IUserService userService)
        {
            try
            {
                return await userService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        public async Task<bool> UpdateUser(
            Guid id,
            UserUpdateDto input,
            [Service] IUserService userService)
        {
            try
            {
                return await userService.UpdateAsync(id, input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        public async Task<bool> DeleteUser(
            Guid id,
            [Service] IUserService userService)
        {
            try
            {
                return await userService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
