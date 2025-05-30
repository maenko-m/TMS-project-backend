using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Milestone;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class MilestoneMutation
    {
        /// <summary>
        /// Creates a new milestone using the provided input data.
        /// </summary>
        /// <param name="input">The data required to create a milestone.</param>
        /// <param name="milestoneService">The service used to manage milestones.</param>
        /// <returns>True if the milestone was successfully created; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during milestone creation.</exception>
        [Authorize]
        public async Task<bool> CreateMilestone(
        MilestoneCreateDto input,
        [Service] IMilestoneService milestoneService)
        {
            try
            {
                return await milestoneService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Updates an existing milestone identified by its unique identifier with the provided data.
        /// </summary>
        /// <param name="id">The unique identifier of the milestone to update.</param>
        /// <param name="user">The authenticated user performing the update, used for access validation.</param>
        /// <param name="input">The updated milestone data.</param>
        /// <param name="milestoneService">The service used to manage milestones.</param>
        /// <returns>True if the milestone was successfully updated; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during milestone update.</exception>
        [Authorize]
        public async Task<bool> UpdateMilestone(
            Guid id,
            ClaimsPrincipal user,
            MilestoneUpdateDto input,
            [Service] IMilestoneService milestoneService)
        {
            try
            {
                var userId = user.GetUserId();

                return await milestoneService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Deletes a milestone identified by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the milestone to delete.</param>
        /// <param name="user">The authenticated user performing the deletion, used for access validation.</param>
        /// <param name="milestoneService">The service used to manage milestones.</param>
        /// <returns>True if the milestone was successfully deleted; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during milestone deletion.</exception>
        [Authorize]
        public async Task<bool> DeleteMilestone(
            Guid id,
            ClaimsPrincipal user,
            [Service] IMilestoneService milestoneService)
        {
            try
            {
                var userId = user.GetUserId();

                return await milestoneService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
