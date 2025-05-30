using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Dtos.Tag;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class TagQuery
    {
        /// <summary>
        /// Retrieves a paginated, sortable list of tags with optional filtering.
        /// </summary>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="tagService">The service used to retrieve tag data.</param>
        /// <param name="filter">Optional filtering criteria to narrow down results.</param>
        /// <returns>A queryable collection of <see cref="TagOutputDto"/> representing the tags.</returns>
        /// <remarks>Authorization is required. Accessible to all authenticated users.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving tags.</exception>
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize]
        public IQueryable<TagOutputDto> GetTags(
            ClaimsPrincipal user,
            [Service] ITagService tagService,
            TagFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var tags = tagService.GetAll();

                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        tags = tags.Where(p => p.Name.Contains(filter.Name));
                    }
                }

                return tags;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a specific tag by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the tag to retrieve.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="tagService">The service used to retrieve tag data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="TagOutputDto"/>.</returns>
        /// <remarks>Authorization is required. Accessible to all authenticated users.</remarks>
        /// <exception cref="GraphQLException">Thrown when the tag cannot be found or access is denied.</exception>
        [Authorize]
        public async Task<TagOutputDto> GetTagById(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITagService tagService)
        {
            try
            {
                var userId = user.GetUserId();

                return await tagService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
