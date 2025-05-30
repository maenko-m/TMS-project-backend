using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestPlan;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class TestPlanQuery
    {
        /// <summary>
        /// Retrieves a paginated, sortable list of all test plans with optional filtering.
        /// </summary>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testPlanService">The service used to retrieve test plan data.</param>
        /// <param name="filter">Optional filtering criteria to narrow down results.</param>
        /// <returns>A queryable collection of <see cref="TestPlanOutputDto"/> representing the test plans.</returns>
        /// <remarks>Authorization is required. Only users with the "Admin" role can access this query.</remarks>
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<TestPlanOutputDto> GetTestPlans(
        ClaimsPrincipal user,
        [Service] ITestPlanService testPlanService,
        TestPlanFilterInput? filter)
        {
            var userId = user.GetUserId();

            var plans = testPlanService.GetAll(userId);

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    plans = plans.Where(p => p.Name.Contains(filter.Name));
                }
            }

            return plans;
        }

        /// <summary>
        /// Retrieves a paginated, sortable list of test plans for a specific project with optional filtering.
        /// </summary>
        /// <param name="projectId">The ID of the project to retrieve test plans from.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testPlanService">The service used to retrieve test plan data.</param>
        /// <param name="filter">Optional filtering criteria to narrow down results.</param>
        /// <returns>A queryable collection of <see cref="TestPlanOutputDto"/> for the specified project.</returns>
        /// <remarks>Authorization is required. Accessible to all authenticated users with permission to the specified project.</remarks>
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize]
        public IQueryable<TestPlanOutputDto> GetTestPlansByProjectId(
            Guid projectId,
            ClaimsPrincipal user,
            [Service] ITestPlanService testPlanService,
            TestPlanFilterInput? filter)
        {
            var userId = user.GetUserId();

            var plans = testPlanService.GetAllByProjectId(projectId, userId);

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    plans = plans.Where(p => p.Name.Contains(filter.Name));
                }
            }

            return plans;
        }

        /// <summary>
        /// Retrieves a specific test plan by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the test plan to retrieve.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testPlanService">The service used to retrieve test plan data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="TestPlanOutputDto"/>.</returns>
        /// <remarks>Authorization is required. Accessible to all authenticated users with permission to the resource.</remarks>
        [Authorize]
        public async Task<TestPlanOutputDto> GetTestPlanById(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestPlanService testPlanService)
        {
            var userId = user.GetUserId();

            return await testPlanService.GetByIdAsync(id, userId);
        }
    }
}
