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
