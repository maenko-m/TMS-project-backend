using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Dtos.TestSuite;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class TestSuiteQuery
    {
        /// <summary>
        /// Retrieves a paginated, sortable list of all test suites with optional filtering.
        /// </summary>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testSuiteService">The service used to retrieve test suite data.</param>
        /// <param name="filter">Optional filtering criteria to narrow down results.</param>
        /// <returns>A queryable collection of <see cref="TestSuiteOutputDto"/> representing the test suites.</returns>
        /// <remarks>Authorization is required. Only users with the "Admin" role can access this query.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving test suites.</exception>
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<TestSuiteOutputDto> GetTestSuites(
            ClaimsPrincipal user,
            [Service] ITestSuiteService testSuiteService,
            TestSuiteFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testSuites = testSuiteService.GetAll(userId);

                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        testSuites = testSuites.Where(p => p.Name.Contains(filter.Name));
                    }
                }

                return testSuites;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a paginated, sortable list of test suites filtered by project ID with optional filtering.
        /// </summary>
        /// <param name="projectId">The ID of the project to filter test suites.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testSuiteService">The service used to retrieve test suite data.</param>
        /// <param name="filter">Optional filtering criteria to narrow down results.</param>
        /// <returns>A queryable collection of <see cref="TestSuiteOutputDto"/> representing the test suites.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving test suites.</exception>
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize]
        public IQueryable<TestSuiteOutputDto> GetTestSuitesByProjectId(
            Guid projectId,
            ClaimsPrincipal user,
            [Service] ITestSuiteService testSuiteService,
            TestSuiteFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testSuites = testSuiteService.GetAllByProjectId(projectId, userId);

                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        testSuites = testSuites.Where(p => p.Name.Contains(filter.Name));
                    }
                }

                return testSuites;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a test suite by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the test suite.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testSuiteService">The service used to retrieve test suite data.</param>
        /// <returns>A <see cref="TestSuiteOutputDto"/> representing the requested test suite.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving the test suite.</exception>
        [Authorize]
        public async Task<TestSuiteOutputDto> GetTestSuiteById(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestSuiteService testSuiteService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testSuiteService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
