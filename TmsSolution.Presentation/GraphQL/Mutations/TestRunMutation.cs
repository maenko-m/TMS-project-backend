using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestRun;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TestRunMutation
    {
        [Authorize]
        public async Task<bool> CreateTestRun(
            TestRunCreateDto input,
            [Service] ITestRunService testRunService)
        {
            try
            {
                return await testRunService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> UpdateTestRun(
            Guid id,
            ClaimsPrincipal user,
            TestRunUpdateDto input,
            [Service] ITestRunService testRunService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testRunService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> DeleteTestRun(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestRunService testRunService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testRunService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
