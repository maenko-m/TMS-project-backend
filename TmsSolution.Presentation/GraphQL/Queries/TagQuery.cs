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
