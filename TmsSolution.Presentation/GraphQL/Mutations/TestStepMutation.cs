using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TestStepMutation
    {
        [Authorize]
        public async Task<bool> CreateTestStep(
            TestStepCreateDto input,
            [Service] ITestStepService testStepService)
        {
            try
            {
                return await testStepService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> UpdateTestStep(
            Guid id,
            ClaimsPrincipal user,
            TestStepUpdateDto input,
            [Service] ITestStepService testStepService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testStepService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> DeleteTestStep(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestStepService testStepService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testStepService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
