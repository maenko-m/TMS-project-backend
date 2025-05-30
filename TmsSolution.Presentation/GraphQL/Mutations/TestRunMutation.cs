using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestRun;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TestRunMutation
    {
        /// <summary>
        /// Creates a new test run with the provided input data.
        /// </summary>
        /// <param name="input">The data required to create the test run.</param>
        /// <param name="testRunService">The service responsible for managing test runs.</param>
        /// <returns>True if the test run was created successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during creation.</exception>
        [Authorize]
        public async Task<bool> CreateTestRun(
            TestRunCreateDto input,
            [Service] ITestRunService testRunService)
        {
            try
            {
                return await testRunService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Updates an existing test run identified by its ID using the provided data.
        /// </summary>
        /// <param name="id">The unique identifier of the test run to update.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="input">The updated data for the test run.</param>
        /// <param name="testRunService">The service responsible for managing test runs.</param>
        /// <returns>True if the test run was updated successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during update.</exception>
        [Authorize]
        public async Task<bool> UpdateTestRun(
            Guid id,
            ClaimsPrincipal user,
            TestRunUpdateDto input,
            [Service] ITestRunService testRunService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testRunService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Deletes a test run identified by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the test run to delete.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testRunService">The service responsible for managing test runs.</param>
        /// <returns>True if the test run was deleted successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during deletion.</exception>
        [Authorize]
        public async Task<bool> DeleteTestRun(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestRunService testRunService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testRunService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
