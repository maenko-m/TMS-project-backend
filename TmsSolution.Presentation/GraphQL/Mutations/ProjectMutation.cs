using System.ComponentModel.DataAnnotations;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Interfaces;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Presentation.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class ProjectMutation
    {
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

        public async Task<bool> UpdateProject(
            Guid id,
            ProjectUpdateDto input,
            [Service] IProjectService projectService)
        {
            try
            {
                return await projectService.UpdateAsync(id, input);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        public async Task<bool> DeleteProject(
            Guid id,
            [Service] IProjectService projectService)
        {
            try
            {
                return await projectService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
