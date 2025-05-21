using HotChocolate.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TmsSolution.Application.Dtos.User;
using TmsSolution.Application.Interfaces;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class UserQuery
    {
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<UserOutputDto> GetUsers(
            [Service] IUserService userService)
        {
            try
            {
                return userService.GetAll();
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        public async Task<UserOutputDto> GetUserById(
            Guid id,
            [Service] IUserService userService)
        {
            try
            {
                return await userService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<UserOutputDto> Me(
            ClaimsPrincipal user, 
            [Service] IUserService userService)
        {
            try
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
                {
                    throw new GraphQLException("User ID not found in token.");
                }

                return await userService.GetByIdAsync(userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
