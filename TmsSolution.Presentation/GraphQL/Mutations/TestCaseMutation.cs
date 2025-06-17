using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Dtos.TestSuite;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TestCaseMutation
    {
        /// <summary>
        /// Creates a new test case using the specified input data.
        /// </summary>
        /// <param name="input">The data required to create a test case.</param>
        /// <param name="testCaseService">The service used to manage test cases.</param>
        /// <returns>True if the test case was successfully created; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during test case creation.</exception>
        [Authorize]
        public async Task<Guid> CreateTestCase(
            TestCaseCreateDto input,
            [Service] ITestCaseService testCaseService)
        {
            try
            {
                return await testCaseService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Updates an existing test case identified by its unique identifier with the provided data.
        /// </summary>
        /// <param name="id">The unique identifier of the test case to update.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="input">The updated test case data.</param>
        /// <param name="testCaseService">The service used to manage test cases.</param>
        /// <returns>True if the test case was successfully updated; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during test case update.</exception>
        [Authorize]
        public async Task<bool> UpdateTestCase(
            Guid id,
            ClaimsPrincipal user,
            TestCaseUpdateDto input,
            [Service] ITestCaseService testCaseService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testCaseService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Deletes a test case identified by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the test case to delete.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testCaseService">The service used to manage test cases.</param>
        /// <returns>True if the test case was successfully deleted; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during test case deletion.</exception>
        [Authorize]
        public async Task<bool> DeleteTestCase(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestCaseService testCaseService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testCaseService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
