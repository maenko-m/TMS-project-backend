using AutoMapper;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Infrastructure.Data.Repositories;

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

        //public async Task<IEnumerable<ProjectOutputDto>> GetAllAsync(User user)
        //{
        //    if (user == null)
        //        throw new ArgumentNullException(nameof(user));

        //    var projects = await _projectRepository.GetAllAsync();
        //    var accessibleProjects = projects.Where(p => p.HasAccess(user));
        //    return _mapper.Map<IEnumerable<ProjectOutputDto>>(accessibleProjects);
        //}

        public async Task<IEnumerable<ProjectOutputDto>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectOutputDto>>(projects);
        }

        //public async Task<ProjectOutputDto> GetByIdAsync(Guid id, User user)
        //{
        //    if (user == null)
        //        throw new ArgumentNullException(nameof(user));

        //    var project = await _projectRepository.GetByIdAsync(id);
        //    if (!project.HasAccess(user))
        //        throw new UnauthorizedAccessException($"User does not have access to project with ID {id}.");

        //    return _mapper.Map<ProjectOutputDto>(project);
        //}

        public async Task<ProjectOutputDto> GetByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return _mapper.Map<ProjectOutputDto>(project);
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

        public async Task<bool> UpdateAsync(Guid id, ProjectUpdateDto projectDto)
        {
            Validator.Validate(projectDto);

            if (projectDto.ProjectUserIds != null)
                await ValidateUserIds(projectDto.ProjectUserIds);

            var project = await _projectRepository.GetByIdAsync(id);
            _mapper.Map(projectDto, project);

            if (!string.IsNullOrWhiteSpace(projectDto.IconPath))
                project.IconBase64 = await ImageProcessor.ConvertIconToBase64(projectDto.IconPath);

            return await _projectRepository.UpdateAsync(project);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _projectRepository.DeleteAsync(id);
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
