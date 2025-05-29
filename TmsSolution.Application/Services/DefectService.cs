using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Infrastructure.Data.Interfaces;

namespace TmsSolution.Application.Services
{
    public class DefectService : IDefectService
    {
        private readonly IDefectRepository _defectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public DefectService(IDefectRepository defectRepository, IUserRepository userRepository, IProjectRepository projectRepository, IMapper mapper) 
        {
            _defectRepository = defectRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public IQueryable<DefectOutputDto> GetAll(Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            if (user.Role != UserRole.Admin)
                throw new UnauthorizedAccessException($"User does not have access to all projects.");

            return _defectRepository
                .GetAll()
                .Select(d => new DefectOutputDto
                {
                    Id = d.Id,
                    ProjectId = d.ProjectId,
                    TestRunId = d.TestRunId,
                    TestCaseId = d.TestCaseId,
                    Title = d.Title,
                    ActualResult = d.ActualResult,
                    Severity = d.Severity,
                    CreatedAt = d.CreatedAt,
                    UpdatedAt = d.UpdatedAt,
                    CreatedById = d.CreatedById
                });
        }

        public IQueryable<DefectOutputDto> GetAllByProjectId(Guid projectId, Guid userId)
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

            return _defectRepository
                .GetAll()
                .Where(d => d.ProjectId == projectId)
                .Select(d => new DefectOutputDto
                {
                    Id = d.Id,
                    ProjectId = d.ProjectId,
                    TestRunId = d.TestRunId,
                    TestCaseId = d.TestCaseId,
                    Title = d.Title,
                    ActualResult = d.ActualResult,
                    Severity = d.Severity,
                    CreatedAt = d.CreatedAt,
                    UpdatedAt = d.UpdatedAt,
                    CreatedById = d.CreatedById
                });
        }

        public async Task<DefectOutputDto> GetByIdAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var defect = await _defectRepository.GetByIdAsync(id);

            var project = defect.Project;

            if (user.Role != UserRole.Admin && project.OwnerId != userId && !project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"User does not have access to defect with ID {id}.");

            return _mapper.Map<DefectOutputDto>(defect);
        }

        public async Task<bool> AddAsync(DefectCreateDto defectDto)
        {
            Validator.Validate(defectDto);

            var defect = _mapper.Map<Defect>(defectDto);

            return await _defectRepository.AddAsync(defect);
        }

        public async Task<bool> UpdateAsync(Guid id, DefectUpdateDto defectDto, Guid userId)
        {
            Validator.Validate(defectDto);

            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var defect = await _defectRepository.GetByIdAsync(id);

            var project = defect.Project;

            if (user.Role != UserRole.Admin && project.OwnerId != userId && !project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"User does not have access to defect with ID {id}.");

            _mapper.Map(defectDto, defect);

            return await _defectRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var defect = await _defectRepository.GetByIdAsync(id);
            return await _defectRepository.DeleteAsync(defect);
        }
    }
}
