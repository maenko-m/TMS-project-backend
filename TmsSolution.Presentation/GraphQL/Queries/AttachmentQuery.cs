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
