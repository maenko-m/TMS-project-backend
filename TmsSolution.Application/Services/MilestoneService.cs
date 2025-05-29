using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Milestone;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Infrastructure.Data.Interfaces;

namespace TmsSolution.Application.Services
{
    public class MilestoneService : IMilestoneService
    {
        private readonly IMilestoneRepository _milestoneRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public MilestoneService(IMilestoneRepository milestoneRepository, IUserRepository userRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _milestoneRepository = milestoneRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }


        public IQueryable<MilestoneOutputDto> GetAll(Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            if (user.Role != UserRole.Admin)
                throw new UnauthorizedAccessException($"Current user does not have access to all milestones.");

            return _milestoneRepository
                .GetAll()
                .Select(m => new MilestoneOutputDto
                {
                    Id = m.Id,
                    ProjectId = m.ProjectId,
                    Name = m.Name,
                    Description = m.Description,
                    DueDate = m.DueDate,
                    CreatedAt = m.CreatedAt,
                    UpdatedAt = m.UpdatedAt,
                    TestRunsCount = m.TestRuns.Count(),
                });
        }

        public IQueryable<MilestoneOutputDto> GetAllByProjectId(Guid projectId, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var project = _projectRepository
                .GetAll()
                .FirstOrDefault(p => p.Id == projectId)
                ?? throw new Exception("Project not found");

            if (user.Role != UserRole.Admin && project.OwnerId != userId && !project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"User does not have access to project with ID {projectId}.");

            return _milestoneRepository
                .GetAll()
                .Where(m => m.ProjectId == projectId)
                .Select(m => new MilestoneOutputDto
                {
                    Id = m.Id,
                    ProjectId = m.ProjectId,
                    Name = m.Name,
                    Description = m.Description,
                    DueDate = m.DueDate,
                    CreatedAt = m.CreatedAt,
                    UpdatedAt = m.UpdatedAt,
                    TestRunsCount = m.TestRuns.Count(),
                });
        }

        public async Task<MilestoneOutputDto> GetByIdAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var milestone = await _milestoneRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && milestone.Project.OwnerId != userId && !milestone.Project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"Current user does not have access to milestone with ID {id}.");

            return _mapper.Map<MilestoneOutputDto>(milestone);
        }

        public async Task<bool> AddAsync(MilestoneCreateDto milestoneDto)
        {
            Validator.Validate(milestoneDto);

            var milestone = _mapper.Map<Milestone>(milestoneDto);

            return await _milestoneRepository.AddAsync(milestone);
        }

        public async Task<bool> UpdateAsync(Guid id, MilestoneUpdateDto milestoneDto, Guid userId)
        {
            Validator.Validate(milestoneDto);

            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var milestone = await _milestoneRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && milestone.Project.OwnerId != userId && !milestone.Project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"Current user does not have access to milestone with ID {id}.");

            _mapper.Map(milestoneDto, milestone);

            return await _milestoneRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var milestone = await _milestoneRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && milestone.Project.OwnerId != userId && !milestone.Project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"Current user does not have access to milestone with ID {id}.");

            return await _milestoneRepository.DeleteAsync(milestone);
        }

    }
}
