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
