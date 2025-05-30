using HotChocolate.Authorization;
using System.Security.Claims;
using TmsSolution.Application.Dtos.Attachment;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Services;
using TmsSolution.Domain.Entities;
using TmsSolution.Presentation.Common.Extensions;

namespace TmsSolution.Presentation.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class AttachmentQuery
    {
        /// <summary>
        /// Retrieves a paginated, filterable, and sortable list of all attachments in the system.
        /// </summary>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="attachmentService">The attachment service used to retrieve attachments.</param>
        /// <returns>
        /// A queryable collection of <see cref="AttachmentOutputDto"/> representing all available attachments.
        /// </returns>
        /// <remarks>
        /// Authorization is required. Only users with the "Admin" role can access this query.
        /// </remarks>
        /// <exception cref="GraphQLException">
        /// Thrown when an error occurs during attachment retrieval.
        /// </exception>
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        [Authorize(Roles = new[] { "Admin" })]
        public IQueryable<AttachmentOutputDto> GetAttachments(
            ClaimsPrincipal user,
            [Service] IAttachmentService attachmentService)
        {
            try
            {
                var userId = user.GetUserId();

                return attachmentService.GetAll(userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
            
        }

        /// <summary>
        /// Retrieves a paginated, filterable, and sortable list of attachments for a specific project.
        /// </summary>
        /// <param name="projectId">The ID of the project whose attachments are being requested.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="attachmentService">The attachment service used to retrieve attachments.</param>
        /// <returns>
        /// A queryable collection of <see cref="AttachmentOutputDto"/> for the specified project.
        /// </returns>
        /// <remarks>
        /// Authorization is required. Accessible to all authenticated users.
        /// </remarks>
        /// <exception cref="GraphQLException">
        /// Thrown when an error occurs during attachment retrieval.
        /// </exception>
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<AttachmentOutputDto> GetAttachmentsByProjectId(
            Guid projectId,
            ClaimsPrincipal user,
            [Service] IAttachmentService attachmentService)
        {
            try
            {
                var userId = user.GetUserId();

                return attachmentService.GetAllByProjectId(projectId, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }

        }

        /// <summary>
        /// Retrieves a single attachment by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the attachment to retrieve.</param>
        /// <param name="user">The authenticated user, used for access validation.</param>
        /// <param name="attachmentService">The attachment service used to retrieve the attachment.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the <see cref="AttachmentOutputDto"/>.
        /// </returns>
        /// <remarks>
        /// Authorization is required. Accessible to all authenticated users.
        /// </remarks>
        /// <exception cref="GraphQLException">
        /// Thrown when the attachment cannot be found or access is denied.
        /// </exception>
        public async Task<AttachmentOutputDto> GetAttachmentByIdAsync(
            Guid id,
            ClaimsPrincipal user,
            [Service] IAttachmentService attachmentService)
        {
            try
            {
                var userId = user.GetUserId();

                return await attachmentService.GetByIdAsync(id, userId);
            }
            catch (Exception ex)
            {
                throw new GraphQLException(new Error(ex.Message));
            }
        }
    }
}
