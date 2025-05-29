using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Application.Dtos.TestSuite;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class TestStepQuery
    {
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<TestStepOutputDto> GetTestSteps(
            ClaimsPrincipal user,
            [Service] ITestStepService testStepService,
            TestStepFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testSteps = testStepService.GetAll(userId);

                if (filter != null)
                {
                    if (filter.TestCaseId.HasValue)
                    {
                        testSteps = testSteps.Where(p => p.TestCaseId == filter.TestCaseId.Value);
                    }
                }

                return testSteps;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize]
        public IQueryable<TestStepOutputDto> GetTestStepsByProjectId(
            Guid projectId,
            ClaimsPrincipal user,
            [Service] ITestStepService testStepService,
            TestStepFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testSteps = testStepService.GetAllByProjectId(projectId, userId);

                if (filter != null)
                {
                    if (filter.TestCaseId.HasValue)
                    {
                        testSteps = testSteps.Where(p => p.TestCaseId == filter.TestCaseId.Value);
                    }
                }

                return testSteps;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }


        [Authorize]
        public async Task<TestStepOutputDto> GetTestStepById(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestStepService testStepService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testStepService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
