using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
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
        /// <summary>
        /// Retrieves a paginated, sortable list of projects accessible to the authenticated user with optional filtering.
        /// </summary>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="projectService">The service used to retrieve project data.</param>
        /// <param name="filter">Optional filtering criteria to narrow down the results.</param>
        /// <returns>A queryable collection of <see cref="ProjectOutputDto"/> representing the projects.</returns>
        /// <remarks>Authorization is required. Accessible to all authenticated users with permission to the projects.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs while retrieving projects.</exception>
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

                var projects = projectService.GetAll(userId);
                Console.WriteLine(projects.First().OwnerId);
                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        var pattern = $"%{filter.Name}%";
                        projects = projects.Where(p => EF.Functions.Like(p.Name, pattern));
                    }
                    /*
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
                    }*/
                }

                return projects;
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves a specific project by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the project to retrieve.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="projectService">The service used to retrieve project data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="ProjectOutputDto"/>.</returns>
        /// <remarks>Authorization is required. Accessible to all authenticated users with permission to the project.</remarks>
        /// <exception cref="GraphQLException">Thrown when the project cannot be found or access is denied.</exception>
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
