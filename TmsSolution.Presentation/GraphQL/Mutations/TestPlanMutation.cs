using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestPlan;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TestPlanMutation
    {
        [Authorize]
        public async Task<bool> CreateTestPlan(
        TestPlanCreateDto input,
        [Service] ITestPlanService testPlanService)
        {
            return await testPlanService.AddAsync(input);
        }

        [Authorize]
        public async Task<bool> UpdateTestPlan(
            Guid id,
            TestPlanUpdateDto input,
            ClaimsPrincipal user,
            [Service] ITestPlanService testPlanService)
        {
            var userId = user.GetUserId();

            return await testPlanService.UpdateAsync(id, input, userId);
        }

        [Authorize]
        public async Task<bool> DeleteTestPlan(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestPlanService testPlanService)
        {
            var userId = user.GetUserId();

            return await testPlanService.DeleteAsync(id, userId);
        }
    }
}
