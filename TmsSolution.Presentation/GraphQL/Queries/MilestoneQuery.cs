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
        /// <summary>
        /// Retrieves a paginated, sortable list of all milestones in the system with optional filtering.
        /// </summary>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="milestoneService">The service used to retrieve milestone data.</param>
        /// <param name="filter">Optional filtering criteria to narrow down results.</param>
        /// <returns>A queryable collection of <see cref="MilestoneOutputDto"/> representing the milestones.</returns>
        /// <remarks>Authorization is required. Only users with the "Admin" role can access this query.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving milestones.</exception>
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

        /// <summary>
        /// Retrieves a paginated, sortable list of milestones for a specific project with optional filtering.
        /// </summary>
        /// <param name="projectId">The ID of the project to retrieve milestones from.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="milestoneService">The service used to retrieve milestone data.</param>
        /// <param name="filter">Optional filtering criteria to narrow down results.</param>
        /// <returns>A queryable collection of <see cref="MilestoneOutputDto"/> for the specified project.</returns>
        /// <remarks>Authorization is required. Accessible to all authenticated users with permission to the specified project.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving milestones.</exception>
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

        /// <summary>
        /// Retrieves a specific milestone by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the milestone to retrieve.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="milestoneService">The service used to retrieve milestone data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="MilestoneOutputDto"/>.</returns>
        /// <remarks>Authorization is required. Accessible to all authenticated users with permission to the resource.</remarks>
        /// <exception cref="GraphQLException">Thrown when the milestone cannot be found or access is denied.</exception>
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
