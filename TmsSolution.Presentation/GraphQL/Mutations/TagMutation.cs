using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Tag;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Interfaces;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TagMutation
    {
        [Authorize]
        public async Task<bool> CreateTag(
            TagCreateDto input,
            [Service] ITagService tagService)
        {
            try
            {
                return await tagService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> UpdateTag(
            Guid id,
            TagUpdateDto input,
            [Service] ITagService tagService)
        {
            try
            {
                return await tagService.UpdateAsync(id, input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> DeleteTag(
            Guid id,
            [Service] ITagService tagService)
        {
            try
            {
                return await tagService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
