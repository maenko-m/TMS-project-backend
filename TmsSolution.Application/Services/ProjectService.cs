using AutoMapper;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Infrastructure.Data.Interfaces;

namespace TmsSolution.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IUserRepository userRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IQueryable<ProjectOutputDto> GetAll(Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var allProjects = _projectRepository.GetAll();

            if (user.Role == UserRole.Admin)
            {
                return allProjects.Select(p => new ProjectOutputDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    IconBase64 = p.IconBase64,
                    AccessType = p.AccessType,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    OwnerId = p.OwnerId,
                    OwnerFullName = p.Owner != null ? $"{p.Owner.FirstName} {p.Owner.LastName}" : "",
                    ProjectUsersCount = p.ProjectUsers.Count,
                    TestCasesCount = p.TestCases.Count,
                    DefectsCount = p.Defects.Count
                });
            }

            return allProjects
                .Where(p =>
                    p.OwnerId == userId ||
                    p.AccessType == ProjectAccessType.Public ||
                    p.ProjectUsers.Any(u => u.Id == userId)
                )
                .Select(p => new ProjectOutputDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    IconBase64 = p.IconBase64,
                    AccessType = p.AccessType,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    OwnerId = p.OwnerId,
                    OwnerFullName = p.Owner != null ? $"{p.Owner.FirstName} {p.Owner.LastName}" : "",
                    ProjectUsersCount = p.ProjectUsers.Count,
                    ProjectUserIds = p.ProjectUsers.Select(pu => pu.Id).ToList(),
                    TestCasesCount = p.TestCases.Count,
                    DefectsCount = p.Defects.Count
                });
        }

        public async Task<ProjectOutputDto> GetByIdAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var project = await _projectRepository.GetByIdAsync(id);
            if (!project.HasAccess(userId))
                throw new UnauthorizedAccessException($"User does not have access to project with ID {id}.");

            var data = _mapper.Map<ProjectOutputDto>(project);

            data.ProjectUserIds = project.ProjectUsers.Select(pu => pu.Id).ToList();
            
            return data;
        }

        public async Task<bool> AddAsync(ProjectCreateDto projectDto)
        {
            Validator.Validate(projectDto);

            if (projectDto.ProjectUserIds != null)
                await ValidateUserIds(projectDto.ProjectUserIds);

            var project = _mapper.Map<Project>(projectDto);

            if (!string.IsNullOrWhiteSpace(projectDto.IconPath))
                project.IconBase64 = await ImageProcessor.ConvertIconToBase64(projectDto.IconPath);

            return await _projectRepository.AddAsync(project);
        }

        public async Task<bool> UpdateAsync(Guid id, ProjectUpdateDto projectDto, Guid userId)
        {
            Validator.Validate(projectDto);

            if (projectDto.ProjectUserIds != null)
                await ValidateUserIds(projectDto.ProjectUserIds);

            var project = await _projectRepository.GetByIdAsync(id);

            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            if (user.Role != UserRole.Admin && project.OwnerId != userId)
                throw new UnauthorizedAccessException($"User does not have access to project with ID {id}.");

            _mapper.Map(projectDto, project);

            if (!string.IsNullOrWhiteSpace(projectDto.IconPath))
                project.IconBase64 = await ImageProcessor.ConvertIconToBase64(projectDto.IconPath);

            return await _projectRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var project = await _projectRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && project.OwnerId != userId)
                throw new UnauthorizedAccessException($"User does not have access to project with ID {id}.");

            return await _projectRepository.DeleteAsync(project);
        }

        private async Task ValidateUserIds(List<Guid> userIds)
        {
            foreach (var userId in userIds)
            {
                if (!await _userRepository.ExistsAsync(userId))
                    throw new Exception($"User with ID {userId} does not exist.");
            }
        }
    }
}
