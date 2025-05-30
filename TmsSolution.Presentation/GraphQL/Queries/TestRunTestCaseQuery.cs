using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestRunTestCase;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class TestRunTestCaseQuery
    {
        /// <summary>
        /// Retrieves a paged, filtered, and sorted list of test run test cases associated with a specific test run ID.
        /// Requires user authorization and checks access to the requested test run resources.
        /// </summary>
        /// <param name="testRunId">The unique identifier of the test run to retrieve test cases for.</param>
        /// <param name="user">The claims principal representing the user, used for resource access validation.</param>
        /// <param name="testRunTestCaseService">The service to access test run test case data.</param>
        /// <returns>An <see cref="IQueryable{TestRunTestCaseOutputDto}"/> representing the filtered test run test cases.</returns>
        /// <exception cref="GraphQLException">Throws when an error occurs during retrieval.</exception>
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize]
        public IQueryable<TestRunTestCaseOutputDto> GetTestRunTestCasesByTestRunId(
            Guid testRunId,
            ClaimsPrincipal user,
            [Service] ITestRunTestCaseService testRunTestCaseService)
        {
            try
            {
                var userId = user.GetUserId();

                var testRunTestCases = testRunTestCaseService.GetAllByTestRunId(testRunId, userId);

                return testRunTestCases;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a single test run test case by its unique identifier.
        /// Requires user authorization and verifies user access to the specific test run test case resource.
        /// </summary>
        /// <param name="id">The unique identifier of the test run test case.</param>
        /// <param name="user">The claims principal representing the user, used for resource access validation.</param>
        /// <param name="testRunTestCaseService">The service to access test run test case data.</param>
        /// <returns>A <see cref="TestRunTestCaseOutputDto"/> representing the requested test run test case.</returns>
        /// <exception cref="GraphQLException">Throws when an error occurs during retrieval.</exception>
        [Authorize]
        public async Task<TestRunTestCaseOutputDto> GetTestRunTestCaseById(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestRunTestCaseService testRunTestCaseService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testRunTestCaseService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
