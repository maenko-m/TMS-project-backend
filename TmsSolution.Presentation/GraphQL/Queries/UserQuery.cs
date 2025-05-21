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
        public async Task<IEnumerable<UserOutputDto>> GetUsers(
            [Service] IUserService userService)
        {
            try
            {
                return await userService.GetAllAsync();
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
    }
}
