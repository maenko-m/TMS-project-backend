using HotChocolate.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TmsSolution.Application.Dtos.User;
using TmsSolution.Application.Interfaces;
using TmsSolution.Domain.Entities;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class UserQuery
    {
        /// <summary>
        /// Retrieves a paginated, filterable, and sortable list of users accessible by the authenticated user.
        /// </summary>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="userService">The service used to retrieve user data.</param>
        /// <returns>A queryable collection of <see cref="UserOutputDto"/> representing the users.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving users.</exception>
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<UserOutputDto> GetUsers(
            ClaimsPrincipal user,
            [Service] IUserService userService)
        {
            try
            {
                var userId = user.GetUserId();

                return userService.GetAll(userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="userService">The service used to retrieve user data.</param>
        /// <returns>A <see cref="UserOutputDto"/> representing the requested user.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving the user.</exception>
        [Authorize]
        public async Task<UserOutputDto> GetUserById(
            Guid id,
            ClaimsPrincipal user,
            [Service] IUserService userService)
        {
            try
            {
                var userId = user.GetUserId();

                return await userService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves the current authenticated user's information.
        /// </summary>
        /// <param name="user">The authenticated user.</param>
        /// <param name="userService">The service used to retrieve user data.</param>
        /// <returns>A <see cref="UserOutputDto"/> representing the current user.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving the current user.</exception>
        [Authorize]
        [Authorize]
        public async Task<UserOutputDto> Me(
            ClaimsPrincipal user, 
            [Service] IUserService userService)
        {
            try
            {
                var userId = user.GetUserId();

                return await userService.GetByIdAsync(userId, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
