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
        /// <summary>
        /// Creates a new tag with the specified input data.
        /// </summary>
        /// <param name="input">The data required to create a tag.</param>
        /// <param name="tagService">The service used to manage tags.</param>
        /// <returns>True if the tag was successfully created; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during tag creation.</exception>
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

        /// <summary>
        /// Updates an existing tag identified by its unique identifier with the provided data.
        /// </summary>
        /// <param name="id">The unique identifier of the tag to update.</param>
        /// <param name="input">The updated tag data.</param>
        /// <param name="tagService">The service used to manage tags.</param>
        /// <returns>True if the tag was successfully updated; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during tag update.</exception>
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

        /// <summary>
        /// Deletes a tag identified by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the tag to delete.</param>
        /// <param name="tagService">The service used to manage tags.</param>
        /// <returns>True if the tag was successfully deleted; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during tag deletion.</exception>
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
