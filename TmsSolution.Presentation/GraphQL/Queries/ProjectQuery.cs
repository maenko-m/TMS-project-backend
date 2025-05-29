using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Interfaces;
using TmsSolution.Domain.Entities;
using TmsSolution.Presentation.Common.Extensions;
using TmsSolution.Presentation.GraphQL.Filters;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class ProjectQuery
    {
        [UsePaging]
        [UseProjection]
        [UseSorting]
        [Authorize]
        public IQueryable<ProjectOutputDto> GetProjects(
            ClaimsPrincipal user,
            [Service] IProjectService projectService,
            ProjectFilterInput? filter)
        {
            try
            {
                var userId = user.GetUserId();

                var projects =  projectService.GetAll(userId);

                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        projects = projects.Where(p => p.Name.Contains(filter.Name));
                    }

                    if (filter.Involvement != null)
                    {
                        switch (filter.Involvement)
                        {
                            case ProjectInvolvement.Owner:
                                projects = projects.Where(p => p.OwnerId == userId);
                                break;
                            case ProjectInvolvement.Participant:
                                projects = projects.Where(p => p.OwnerId != userId);
                                break;
                        }
                    }
                }

                return projects;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        [Authorize]
        public async Task<ProjectOutputDto> GetProjectById(
            Guid id,
            ClaimsPrincipal user,
            [Service] IProjectService projectService)
        {
            try
            {
                var userId = user.GetUserId();

                return await projectService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
