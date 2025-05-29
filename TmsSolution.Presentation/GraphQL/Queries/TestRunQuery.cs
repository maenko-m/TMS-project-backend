using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestRun;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class TestRunQuery
    {
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<TestRunOutputDto> GetTestRuns(
            ClaimsPrincipal user,
            [Service] ITestRunService testRunService,
            TestRunFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testRuns = testRunService.GetAll(userId);

                if (filter != null)
                {
                    if (!string.IsNullOrWhiteSpace(filter.Name))
                    {
                        testRuns = testRuns.Where(r => r.Name.Contains(filter.Name));
                    }

                    if (filter.IsCompleted.HasValue)
                    {
                        if (filter.IsCompleted.Value)
                            testRuns = testRuns.Where(r => r.EndTime != null);
                        else
                            testRuns = testRuns.Where(r => r.EndTime == null);
                    }

                    if (filter.MilestoneId.HasValue)
                    {
                        testRuns = testRuns.Where(r => r.MilestoneId == filter.MilestoneId.Value);
                    }

                    if (filter.Status.HasValue)
                    {
                        testRuns = testRuns.Where(r => r.Status == filter.Status.Value);
                    }
                }

                return testRuns;
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
        public IQueryable<TestRunOutputDto> GetTestRunsByProjectId(
            Guid projectId,
            ClaimsPrincipal user,
            [Service] ITestRunService testRunService,
            TestRunFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testRuns = testRunService.GetAllByProjectId(projectId, userId);

                if (filter != null)
                {
                    if (!string.IsNullOrWhiteSpace(filter.Name))
                    {
                        testRuns = testRuns.Where(r => r.Name.Contains(filter.Name));
                    }

                    if (filter.IsCompleted.HasValue)
                    {
                        if (filter.IsCompleted.Value)
                            testRuns = testRuns.Where(r => r.EndTime != null);
                        else
                            testRuns = testRuns.Where(r => r.EndTime == null);
                    }

                    if (filter.MilestoneId.HasValue)
                    {
                        testRuns = testRuns.Where(r => r.MilestoneId == filter.MilestoneId.Value);
                    }

                    if (filter.Status.HasValue)
                    {
                        testRuns = testRuns.Where(r => r.Status == filter.Status.Value);
                    }
                }

                return testRuns;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<TestRunOutputDto> GetTestRunById(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestRunService testRunService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testRunService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
