using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Attachment;
using TmsSolution.Application.Dtos.Defect;

namespace TmsSolution.Application.Interfaces
{
    public interface IAttachmentService
    {
        IQueryable<AttachmentOutputDto> GetAll(Guid userId);
        IQueryable<AttachmentOutputDto> GetAllByProjectId(Guid projectId, Guid userId);
        Task<AttachmentOutputDto> GetByIdAsync(Guid id, Guid userId);
        Task<Guid> UploadAsync(AttachmentCreateDto attachmentDto, string uploadsFolder, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
