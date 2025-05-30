using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestRun;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class TestRunQuery
    {
        /// <summary>
        /// Retrieves a paginated, sortable list of all test runs with optional filtering.
        /// </summary>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testRunService">The service used to retrieve test run data.</param>
        /// <param name="filter">Optional filter criteria for the test runs.</param>
        /// <returns>A queryable collection of <see cref="TestRunOutputDto"/>.</returns>
        /// <remarks>Authorization is required. Only users with the "Admin" role can access this query.</remarks>
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<TestRunOutputDto> GetTestRuns(
            ClaimsPrincipal user,
            [Service] ITestRunService testRunService,
            TestRunFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testRuns = testRunService.GetAll(userId);

                if (filter != null)
                {
                    if (!string.IsNullOrWhiteSpace(filter.Name))
                    {
                        testRuns = testRuns.Where(r => r.Name.Contains(filter.Name));
                    }

                    if (filter.IsCompleted.HasValue)
                    {
                        if (filter.IsCompleted.Value)
                            testRuns = testRuns.Where(r => r.EndTime != null);
                        else
                            testRuns = testRuns.Where(r => r.EndTime == null);
                    }

                    if (filter.MilestoneId.HasValue)
                    {
                        testRuns = testRuns.Where(r => r.MilestoneId == filter.MilestoneId.Value);
                    }

                    if (filter.Status.HasValue)
                    {
                        testRuns = testRuns.Where(r => r.Status == filter.Status.Value);
                    }
                }

                return testRuns;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a paginated, sortable list of test runs for a specific project with optional filtering.
        /// </summary>
        /// <param name="projectId">The ID of the project to retrieve test runs for.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testRunService">The service used to retrieve test run data.</param>
        /// <param name="filter">Optional filter criteria for the test runs.</param>
        /// <returns>A queryable collection of <see cref="TestRunOutputDto"/> for the specified project.</returns>
        /// <remarks>Authorization is required. Accessible to all authenticated users with access to the project.</remarks>
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize]
        public IQueryable<TestRunOutputDto> GetTestRunsByProjectId(
            Guid projectId,
            ClaimsPrincipal user,
            [Service] ITestRunService testRunService,
            TestRunFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testRuns = testRunService.GetAllByProjectId(projectId, userId);

                if (filter != null)
                {
                    if (!string.IsNullOrWhiteSpace(filter.Name))
                    {
                        testRuns = testRuns.Where(r => r.Name.Contains(filter.Name));
                    }

                    if (filter.IsCompleted.HasValue)
                    {
                        if (filter.IsCompleted.Value)
                            testRuns = testRuns.Where(r => r.EndTime != null);
                        else
                            testRuns = testRuns.Where(r => r.EndTime == null);
                    }

                    if (filter.MilestoneId.HasValue)
                    {
                        testRuns = testRuns.Where(r => r.MilestoneId == filter.MilestoneId.Value);
                    }

                    if (filter.Status.HasValue)
                    {
                        testRuns = testRuns.Where(r => r.Status == filter.Status.Value);
                    }
                }

                return testRuns;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a specific test run by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the test run to retrieve.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testRunService">The service used to retrieve test run data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="TestRunOutputDto"/>.</returns>
        /// <remarks>Authorization is required. Accessible to all authenticated users with access to the resource.</remarks>
        [Authorize]
        public async Task<TestRunOutputDto> GetTestRunById(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestRunService testRunService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testRunService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
