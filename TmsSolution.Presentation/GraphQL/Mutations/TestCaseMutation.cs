using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Dtos.TestSuite;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TestCaseMutation
    {
        [Authorize]
        public async Task<bool> CreateTestCase(
            TestCaseCreateDto input,
            [Service] ITestCaseService testCaseService)
        {
            try
            {
                return await testCaseService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> UpdateTestCase(
            Guid id,
            ClaimsPrincipal user,
            TestCaseUpdateDto input,
            [Service] ITestCaseService testCaseService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testCaseService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> DeleteTestCase(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestCaseService testCaseService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testCaseService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
