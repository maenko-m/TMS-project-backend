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
        /// <summary>
        /// Creates a new user with the specified input data.
        /// </summary>
        /// <param name="input">The data required to create the user.</param>
        /// <param name="userService">The service responsible for managing users.</param>
        /// <returns>True if the user was created successfully; otherwise, false.</returns>
        /// <exception cref="GraphQLException">Thrown when an error occurs during creation.</exception>
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

        /// <summary>
        /// Updates an existing user identified by its ID using the provided data.
        /// </summary>
        /// <param name="id">The unique identifier of the user to update.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="input">The updated data for the user.</param>
        /// <param name="userService">The service responsible for managing users.</param>
        /// <returns>True if the user was updated successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during update.</exception>
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

        /// <summary>
        /// Deletes an existing user identified by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="userService">The service responsible for managing users.</param>
        /// <returns>True if the user was deleted successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during deletion.</exception>
        [Authorize]
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
