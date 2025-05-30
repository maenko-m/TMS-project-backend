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
        /// <summary>
        /// Creates a new test suite with the specified input data.
        /// </summary>
        /// <param name="input">The data required to create the test suite.</param>
        /// <param name="testSuiteService">The service responsible for managing test suites.</param>
        /// <returns>True if the test suite was created successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during creation.</exception>
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

        /// <summary>
        /// Updates an existing test suite identified by its ID using the provided data.
        /// </summary>
        /// <param name="id">The unique identifier of the test suite to update.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="input">The updated data for the test suite.</param>
        /// <param name="testSuiteService">The service responsible for managing test suites.</param>
        /// <returns>True if the test suite was updated successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during update.</exception>
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

        /// <summary>
        /// Deletes a test suite identified by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the test suite to delete.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testSuiteService">The service responsible for managing test suites.</param>
        /// <returns>True if the test suite was deleted successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during deletion.</exception>
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
