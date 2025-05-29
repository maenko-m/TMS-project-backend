using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.User;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class UserMutation
    {
        [Authorize]
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

        [Authorize]
        public async Task<bool> UpdateUser(
            Guid id,
            ClaimsPrincipal user,
            UserUpdateDto input,
            [Service] IUserService userService)
        {
            try
            {
                var userId = user.GetUserId();

                return await userService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        public async Task<bool> DeleteUser(
            Guid id,
            ClaimsPrincipal user,
            [Service] IUserService userService)
        {
            try
            {
                var userId = user.GetUserId();

                return await userService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
