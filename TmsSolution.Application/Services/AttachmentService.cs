using AutoMapper;
using TmsSolution.Application.Dtos.Attachment;
using TmsSolution.Application.Interfaces;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Infrastructure.Data.Interfaces;

namespace TmsSolution.Application.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AttachmentService(IAttachmentRepository attachmentRepository, IProjectRepository projectRepository, IUserRepository userRepository, IMapper mapper)
        {
            _attachmentRepository = attachmentRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IQueryable<AttachmentOutputDto> GetAll(Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            if (user.Role != UserRole.Admin)
                throw new UnauthorizedAccessException($"User does not have access to all projects.");

            return _attachmentRepository
                .GetAll()
                .Select(a => new AttachmentOutputDto
                {
                    Id = a.Id,
                    ProjectId = a.ProjectId,
                    FileName = a.FileName,
                    FileUrl = a.FileUrl,
                    FileSize = a.FileSize,
                    ContentType = a.ContentType,
                    CreatedAt = a.CreatedAt,
                    UploadedById = a.UploadedById,
                });
        }

        public IQueryable<AttachmentOutputDto> GetAllByProjectId(Guid projectId, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var project = _projectRepository
                .GetAll()
                .FirstOrDefault(p => p.Id == projectId)
                ?? throw new Exception("Project not found");

            if (user.Role != UserRole.Admin && project.OwnerId != userId && !project.ProjectUsers.Any(pu => pu.UserId == userId))
                throw new UnauthorizedAccessException($"User does not have access to project with ID {projectId}.");

            return _attachmentRepository
                .GetAll()
                .Select(a => new AttachmentOutputDto
                {
                    Id = a.Id,
                    ProjectId = a.ProjectId,
                    FileName = a.FileName,
                    FileUrl = a.FileUrl,
                    FileSize = a.FileSize,
                    ContentType = a.ContentType,
                    CreatedAt = a.CreatedAt,
                    UploadedById = a.UploadedById,
                });
        }

        public async Task<AttachmentOutputDto> GetByIdAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var attachment = await _attachmentRepository.GetByIdAsync(id);

            var project = attachment.Project;

            if (user.Role != UserRole.Admin && project.OwnerId != userId && !project.ProjectUsers.Any(pu => pu.UserId == userId))
                throw new UnauthorizedAccessException($"User does not have access to attachment with ID {id}.");

            return _mapper.Map<AttachmentOutputDto>(attachment);
        }

        public async Task<Guid> UploadAsync(AttachmentCreateDto attachmentDto, string uploadsFolder, Guid userId)
        {
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Path.GetFileName(attachmentDto.File.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await attachmentDto.File.CopyToAsync(stream);
            }

            var attachment = new Attachment
            {
                ProjectId = attachmentDto.ProjectId,
                FileName = fileName,
                FileUrl = Path.Combine("Uploads", attachmentDto.ProjectId.ToString(), fileName).Replace("\\", "/"),
                FileSize = attachmentDto.File.Length,
                ContentType = attachmentDto.File.ContentType,
                CreatedAt = DateTime.UtcNow,
                UploadedById = userId
            };

            if (await _attachmentRepository.AddAsync(attachment))
                return attachment.Id;
            else
                return Guid.Empty;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var attachment = await _attachmentRepository.GetByIdAsync(id);

            var project = attachment.Project;

            if (user.Role != UserRole.Admin && project.OwnerId != userId && !project.ProjectUsers.Any(pu => pu.UserId == userId))
                throw new UnauthorizedAccessException($"User does not have access to attachment with ID {id}.");

            return await _attachmentRepository.DeleteAsync(attachment);
        }
    }
}
