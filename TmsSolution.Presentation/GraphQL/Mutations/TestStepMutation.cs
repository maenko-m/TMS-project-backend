using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TestStepMutation
    {
        /// <summary>
        /// Creates a new test step using the provided input data.
        /// </summary>
        /// <param name="input">The data required to create the test step.</param>
        /// <param name="testStepService">The service responsible for managing test steps.</param>
        /// <returns>True if the test step was created successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during creation.</exception>
        [Authorize]
        public async Task<bool> CreateTestStep(
            TestStepCreateDto input,
            [Service] ITestStepService testStepService)
        {
            try
            {
                return await testStepService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Updates an existing test step identified by its ID using the provided data.
        /// </summary>
        /// <param name="id">The unique identifier of the test step to update.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="input">The updated data for the test step.</param>
        /// <param name="testStepService">The service responsible for managing test steps.</param>
        /// <returns>True if the test step was updated successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during update.</exception>
        [Authorize]
        public async Task<bool> UpdateTestStep(
            Guid id,
            ClaimsPrincipal user,
            TestStepUpdateDto input,
            [Service] ITestStepService testStepService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testStepService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }


        /// <summary>
        /// Deletes a test step identified by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the test step to delete.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testStepService">The service responsible for managing test steps.</param>
        /// <returns>True if the test step was deleted successfully; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during deletion.</exception>
        [Authorize]
        public async Task<bool> DeleteTestStep(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestStepService testStepService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testStepService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
