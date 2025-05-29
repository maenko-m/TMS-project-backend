using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Milestone;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class MilestoneQuery
    {
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<MilestoneOutputDto> GetMilestones(
            ClaimsPrincipal user,
            [Service] IMilestoneService milestoneService,
            MilestoneFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var milestones = milestoneService.GetAll(userId);

                if (filter != null)
                {
                    if (!string.IsNullOrWhiteSpace(filter.Name))
                        milestones = milestones.Where(m => m.Name.Contains(filter.Name));

                    if (filter.IsHavingTestRuns.HasValue)
                    {
                        if (filter.IsHavingTestRuns.Value)
                            milestones = milestones.Where(m => m.TestRunsCount > 0);
                        else
                            milestones = milestones.Where(m => m.TestRunsCount == 0);
                    }

                    if (filter.IsActual.HasValue)
                    {
                        if (filter.IsActual.Value)
                            milestones = milestones.Where(m => m.DueDate == null);
                        else
                            milestones = milestones.Where(m => m.DueDate != null && m.DueDate.Value < DateTime.Now);
                    }
                }

                return milestones;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize]
        public IQueryable<MilestoneOutputDto> GetMilestonesByProjectId(
            Guid projectId,
            ClaimsPrincipal user,
            [Service] IMilestoneService milestoneService,
            MilestoneFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var milestones = milestoneService.GetAllByProjectId(projectId, userId);

                if (filter != null)
                {
                    if (!string.IsNullOrWhiteSpace(filter.Name))
                        milestones = milestones.Where(m => m.Name.Contains(filter.Name));

                    if (filter.IsHavingTestRuns.HasValue)
                    {
                        if (filter.IsHavingTestRuns.Value)
                            milestones = milestones.Where(m => m.TestRunsCount > 0);
                        else
                            milestones = milestones.Where(m => m.TestRunsCount == 0);
                    }

                    if (filter.IsActual.HasValue)
                    {
                        if (filter.IsActual.Value)
                            milestones = milestones.Where(m => m.DueDate == null);
                        else
                            milestones = milestones.Where(m => m.DueDate != null && m.DueDate.Value < DateTime.Now);
                    }
                }

                return milestones;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<MilestoneOutputDto> GetMilestoneById(
            Guid id,
            ClaimsPrincipal user,
            [Service] IMilestoneService milestoneService)
        {
            try
            {
                var userId = user.GetUserId();

                return await milestoneService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
