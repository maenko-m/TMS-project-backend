using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestPlan;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class TestPlanQuery
    {
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<TestPlanOutputDto> GetTestPlans(
        ClaimsPrincipal user,
        [Service] ITestPlanService testPlanService,
        TestPlanFilterInput? filter)
        {
            var userId = user.GetUserId();

            var plans = testPlanService.GetAll(userId);

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    plans = plans.Where(p => p.Name.Contains(filter.Name));
                }
            }

            return plans;
        }

        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize]
        public IQueryable<TestPlanOutputDto> GetTestPlansByProjectId(
            Guid projectId,
            ClaimsPrincipal user,
            [Service] ITestPlanService testPlanService,
            TestPlanFilterInput? filter)
        {
            var userId = user.GetUserId();

            var plans = testPlanService.GetAllByProjectId(projectId, userId);

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    plans = plans.Where(p => p.Name.Contains(filter.Name));
                }
            }

            return plans;
        }

        [Authorize]
        public async Task<TestPlanOutputDto> GetTestPlanById(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestPlanService testPlanService)
        {
            var userId = user.GetUserId();

            return await testPlanService.GetByIdAsync(id, userId);
        }
    }
}
