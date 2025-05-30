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
        /// <summary>
        /// Retrieves a paginated, sortable list of all test cases with optional filtering.
        /// </summary>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testCaseService">The service used to retrieve test case data.</param>
        /// <param name="filter">Optional filtering criteria to narrow down results.</param>
        /// <returns>A queryable collection of <see cref="TestCaseOutputDto"/> representing the test cases.</returns>
        /// <remarks>Authorization is required. Only users with the "Admin" role can access this query.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving test cases.</exception>
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

        /// <summary>
        /// Retrieves a paginated, sortable list of test cases for a specific project with optional filtering.
        /// </summary>
        /// <param name="projectId">The ID of the project to retrieve test cases from.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testCaseService">The service used to retrieve test case data.</param>
        /// <param name="filter">Optional filtering criteria to narrow down results.</param>
        /// <returns>A queryable collection of <see cref="TestCaseOutputDto"/> for the specified project.</returns>
        /// <remarks>Authorization is required. Accessible to all authenticated users with permission to the specified project.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving test cases.</exception>
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

        /// <summary>
        /// Retrieves a specific test case by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the test case to retrieve.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testCaseService">The service used to retrieve test case data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="TestCaseOutputDto"/>.</returns>
        /// <remarks>Authorization is required. Accessible to all authenticated users with permission to the resource.</remarks>
        /// <exception cref="GraphQLException">Thrown when the test case cannot be found or access is denied.</exception>
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
