using HotChocolate.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Interfaces;
using TmsSolution.Domain.Entities;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class ProjectMutation
    {
        [Authorize]
        public async Task<bool> CreateProject(
            ProjectCreateDto input,
            [Service] IProjectService projectService)
        {
            try
            {
                return await projectService.AddAsync(input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> UpdateProject(
            Guid id,
            ClaimsPrincipal user,
            ProjectUpdateDto input,
            [Service] IProjectService projectService)
        {
            try
            {
                var userId = user.GetUserId();

                return await projectService.UpdateAsync(id, input, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<bool> DeleteProject(
            Guid id,
            ClaimsPrincipal user,
            [Service] IProjectService projectService)
        {
            try
            {
                var userId = user.GetUserId();

                return await projectService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
