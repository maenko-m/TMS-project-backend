using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestRunTestCase;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TestRunTestCaseMutation
    {
        /// <summary>
        /// Creates a new test run test case using the provided input data.
        /// </summary>
        /// <param name="input">The data required to create the test run test case.</param>
        /// <param name="testRunTestCaseService">The service responsible for managing test run test cases.</param>
        /// <returns>True if the test run test case was created successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during creation.</exception>
        [Authorize]
        public async Task<bool> CreateTestRunTestCase(
            TestRunTestCaseCreateDto input,
            [Service] ITestRunTestCaseService testRunTestCaseService)
        {
            try
            {
                return await testRunTestCaseService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Updates an existing test run test case identified by its ID using the provided data.
        /// </summary>
        /// <param name="id">The unique identifier of the test run test case to update.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="input">The updated data for the test run test case.</param>
        /// <param name="testRunTestCaseService">The service responsible for managing test run test cases.</param>
        /// <returns>True if the test run test case was updated successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during update.</exception>
        [Authorize]
        public async Task<bool> UpdateTestRunTestCase(
            Guid id,
            ClaimsPrincipal user,
            TestRunTestCaseUpdateDto input,
            [Service] ITestRunTestCaseService testRunTestCaseService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testRunTestCaseService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Deletes a test run test case identified by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the test run test case to delete.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testRunTestCaseService">The service responsible for managing test run test cases.</param>
        /// <returns>True if the test run test case was deleted successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during deletion.</exception>
        [Authorize]
        public async Task<bool> DeleteTestRunTestCase(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestRunTestCaseService testRunTestCaseService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testRunTestCaseService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
