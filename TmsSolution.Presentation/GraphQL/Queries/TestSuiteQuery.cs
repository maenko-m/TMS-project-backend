using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Dtos.TestSuite;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class TestSuiteQuery
    {
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<TestSuiteOutputDto> GetTestSuites(
            ClaimsPrincipal user,
            [Service] ITestSuiteService testSuiteService,
            TestSuiteFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testSuites = testSuiteService.GetAll(userId);

                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        testSuites = testSuites.Where(p => p.Name.Contains(filter.Name));
                    }
                }

                return testSuites;
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
        public IQueryable<TestSuiteOutputDto> GetTestSuitesByProjectId(
            Guid projectId,
            ClaimsPrincipal user,
            [Service] ITestSuiteService testSuiteService,
            TestSuiteFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var testSuites = testSuiteService.GetAllByProjectId(projectId, userId);

                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        testSuites = testSuites.Where(p => p.Name.Contains(filter.Name));
                    }
                }

                return testSuites;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }


        [Authorize]
        public async Task<TestSuiteOutputDto> GetTestSuiteById(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestSuiteService testSuiteService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testSuiteService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
