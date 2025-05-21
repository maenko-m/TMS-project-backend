using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Interfaces;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class ProjectQuery
    {
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ProjectOutputDto> GetProjects(
            [Service] IProjectService projectService)
        {
            try
            {
                return projectService.GetAll();
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        public async Task<ProjectOutputDto> GetProjectById(
            Guid id,
            [Service] IProjectService projectService)
        {
            try
            {
                return await projectService.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
