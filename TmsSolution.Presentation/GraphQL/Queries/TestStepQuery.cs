using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Application.Dtos.TestSuite;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class TestStepQuery
    {
        /// <summary>
        /// Retrieves a paginated, sortable list of all test steps with optional filtering.
        /// </summary>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testStepService">The service used to retrieve test step data.</param>
        /// <param name="filter">Optional filtering criteria to narrow down results.</param>
        /// <returns>A queryable collection of <see cref="TestStepOutputDto"/> representing the test steps.</returns>
        /// <remarks>Authorization is required. Only users with the "Admin" role can access this query.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving test steps.</exception>
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<TestStepOutputDto> GetTestSteps(
            ClaimsPrincipal user,
            [Service] ITestStepService testStepService,
            TestStepFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testSteps = testStepService.GetAll(userId);

                if (filter != null)
                {
                    if (filter.TestCaseId.HasValue)
                    {
                        testSteps = testSteps.Where(p => p.TestCaseId == filter.TestCaseId.Value);
                    }
                }

                return testSteps;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a paginated, sortable list of test steps filtered by project ID with optional filtering.
        /// </summary>
        /// <param name="projectId">The ID of the project to filter test steps.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testStepService">The service used to retrieve test step data.</param>
        /// <param name="filter">Optional filtering criteria to narrow down results.</param>
        /// <returns>A queryable collection of <see cref="TestStepOutputDto"/> representing the test steps.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving test steps.</exception>
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize]
        public IQueryable<TestStepOutputDto> GetTestStepsByProjectId(
            Guid projectId,
            ClaimsPrincipal user,
            [Service] ITestStepService testStepService,
            TestStepFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testSteps = testStepService.GetAllByProjectId(projectId, userId);

                if (filter != null)
                {
                    if (filter.TestCaseId.HasValue)
                    {
                        testSteps = testSteps.Where(p => p.TestCaseId == filter.TestCaseId.Value);
                    }
                }

                return testSteps;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a test step by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the test step.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testStepService">The service used to retrieve test step data.</param>
        /// <returns>A <see cref="TestStepOutputDto"/> representing the requested test step.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving the test step.</exception>
        [Authorize]
        public async Task<TestStepOutputDto> GetTestStepById(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestStepService testStepService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testStepService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
