using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.TestPlan;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Services;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class TestPlanMutation
    {
        /// <summary>
        /// Creates a new test plan using the specified input data.
        /// </summary>
        /// <param name="input">The data required to create a test plan.</param>
        /// <param name="testPlanService">The service used to manage test plans.</param>
        /// <returns>True if the test plan was successfully created; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during creation.</exception>
        [Authorize]
        public async Task<bool> CreateTestPlan(
        TestPlanCreateDto input,
        [Service] ITestPlanService testPlanService)
        {
            try
            {
                return await testPlanService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Updates an existing test plan identified by its unique identifier with the provided data.
        /// </summary>
        /// <param name="id">The unique identifier of the test plan to update.</param>
        /// <param name="input">The updated test plan data.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testPlanService">The service used to manage test plans.</param>
        /// <returns>True if the test plan was successfully updated; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during update.</exception>
        [Authorize]
        public async Task<bool> UpdateTestPlan(
            Guid id,
            TestPlanUpdateDto input,
            ClaimsPrincipal user,
            [Service] ITestPlanService testPlanService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testPlanService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Deletes a test plan identified by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the test plan to delete.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="testPlanService">The service used to manage test plans.</param>
        /// <returns>True if the test plan was successfully deleted; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during deletion.</exception>
        [Authorize]
        public async Task<bool> DeleteTestPlan(
            Guid id,
            ClaimsPrincipal user,
            [Service] ITestPlanService testPlanService)
        {
            try
            {
                var userId = user.GetUserId();

                return await testPlanService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
