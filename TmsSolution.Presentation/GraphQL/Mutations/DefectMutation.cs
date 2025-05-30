using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Interfaces;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class DefectMutation
    {
        /// <summary>
        /// Creates a new defect based on the provided input data.
        /// </summary>
        /// <param name="input">The data required to create a defect.</param>
        /// <param name="defectService">The service used to manage defects.</param>
        /// <returns>True if the defect was successfully created; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during defect creation.</exception>
        [Authorize]
        public async Task<bool> CreateDefect(
            DefectCreateDto input,
            [Service] IDefectService defectService)
        {
            try
            {
                return await defectService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Updates an existing defect identified by its unique identifier with new data.
        /// </summary>
        /// <param name="id">The unique identifier of the defect to update.</param>
        /// <param name="user">The authenticated user performing the update, used for access validation.</param>
        /// <param name="input">The updated defect data.</param>
        /// <param name="defectService">The service used to manage defects.</param>
        /// <returns>True if the defect was successfully updated; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during defect update.</exception>
        [Authorize]
        public async Task<bool> UpdateDefect(
            Guid id,
            ClaimsPrincipal user,
            DefectUpdateDto input,
            [Service] IDefectService defectService)
        {
            try
            {
                var userId = user.GetUserId();

                return await defectService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Deletes a defect identified by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the defect to delete.</param>
        /// <param name="user">The authenticated user performing the deletion, used for access validation.</param>
        /// <param name="defectService">The service used to manage defects.</param>
        /// <returns>True if the defect was successfully deleted; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during defect deletion.</exception>
        [Authorize]
        public async Task<bool> DeleteDefect(
            Guid id,
            ClaimsPrincipal user,
            [Service] IDefectService defectService)
        {
            try
            {
                var userId = user.GetUserId();

                return await defectService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
