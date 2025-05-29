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
