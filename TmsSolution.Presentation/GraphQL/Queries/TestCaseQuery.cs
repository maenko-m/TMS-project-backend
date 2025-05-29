using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Interfaces;
using TmsSolution.Domain.Entities;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class TestCaseQuery
    {
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<TestCaseOutputDto> GetTestCases(
            ClaimsPrincipal user,
            [Service] ITestCaseService testCaseService,
            TestCaseFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testCases = testCaseService.GetAll(userId);

                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Title))
                    {
                        testCases = testCases.Where(p => p.Title.Contains(filter.Title));
                    }

                    if (filter.SuiteId.HasValue)
                    {
                        testCases = testCases.Where(tc => tc.SuiteId == filter.SuiteId.Value);
                    }

                    if (filter.IsHavingDefects.HasValue && filter.IsHavingDefects.Value)
                    {
                        testCases = testCases.Where(tc => tc.Defects.Count > 0);
                    }

                    if (filter.TagIds != null && filter.TagIds.Count != 0)
                    {
                        testCases = testCases.Where(tc =>
                            filter.TagIds.All(tagId =>
                                tc.Tags.Any(tag => tag.Id == tagId)
                            )
                        );
                    }
                }

                return testCases;
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
        public IQueryable<TestCaseOutputDto> GetTestCasesByProjectId(
            Guid projectId,
            ClaimsPrincipal user,
            [Service] ITestCaseService testCaseService,
            TestCaseFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testCases = testCaseService.GetAllByProjectId(projectId, userId);

                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Title))
                    {
                        testCases = testCases.Where(p => p.Title.Contains(filter.Title));
                    }

                    if (filter.SuiteId.HasValue)
                    {
                        testCases = testCases.Where(tc => tc.SuiteId == filter.SuiteId.Value);
                    }

                    if (filter.IsHavingDefects.HasValue && filter.IsHavingDefects.Value)
                    {
                        testCases = testCases.Where(tc => tc.Defects.Count > 0);
                    }

                    if (filter.TagIds != null && filter.TagIds.Count != 0)
                    {
                        testCases = testCases.Where(tc =>
                            filter.TagIds.All(tagId =>
                                tc.Tags.Any(tag => tag.Id == tagId)
                            )
                        );
                    }
                }

                return testCases;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<TestCaseOutputDto> GetTestCaseById(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestCaseService testCaseService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testCaseService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
