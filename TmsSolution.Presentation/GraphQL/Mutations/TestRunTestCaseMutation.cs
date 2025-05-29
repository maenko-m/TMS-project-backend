using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestRunTestCase;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TestRunTestCaseMutation
    {
        [Authorize]
        public async Task<bool> CreateTestRunTestCase(
            TestRunTestCaseCreateDto input,
            [Service] ITestRunTestCaseService testRunTestCaseService)
        {
            try
            {
                return await testRunTestCaseService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> UpdateTestRunTestCase(
            Guid id,
            ClaimsPrincipal user,
            TestRunTestCaseUpdateDto input,
            [Service] ITestRunTestCaseService testRunTestCaseService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testRunTestCaseService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> DeleteTestRunTestCase(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestRunTestCaseService testRunTestCaseService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testRunTestCaseService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
