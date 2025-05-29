using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Dtos.TestSuite;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TestSuiteMutation
    {
        [Authorize]
        public async Task<bool> CreateTestSuite(
            TestSuiteCreateDto input,
            [Service] ITestSuiteService testSuiteService)
        {
            try
            {
                return await testSuiteService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> UpdateTestSuite(
            Guid id,
            ClaimsPrincipal user,
            TestSuiteUpdateDto input,
            [Service] ITestSuiteService testSuiteService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testSuiteService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> DeleteTestSuite(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestSuiteService testSuiteService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testSuiteService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
