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
        /// <summary>
        /// Creates a new project with the specified input data.
        /// </summary>
        /// <param name="input">The data required to create a project.</param>
        /// <param name="projectService">The service used to manage projects.</param>
        /// <returns>True if the project was successfully created; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during project creation.</exception>
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

        /// <summary>
        /// Updates an existing project identified by its unique identifier with the provided data.
        /// </summary>
        /// <param name="id">The unique identifier of the project to update.</param>
        /// <param name="user">The authenticated user performing the update, used for access validation.</param>
        /// <param name="input">The updated project data.</param>
        /// <param name="projectService">The service used to manage projects.</param>
        /// <returns>True if the project was successfully updated; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during project update.</exception>
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

        /// <summary>
        /// Deletes a project identified by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the project to delete.</param>
        /// <param name="user">The authenticated user performing the deletion, used for access validation.</param>
        /// <param name="projectService">The service used to manage projects.</param>
        /// <returns>True if the project was successfully deleted; otherwise, false.</returns>
        /// <remarks>Authorization is required.</remarks>
        /// <exception cref="GraphQLException">Thrown when an error occurs during project deletion.</exception>
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
