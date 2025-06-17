using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.Tag;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Infrastructure.Data.Interfaces;

namespace TmsSolution.Application.Services
{
    public class TestCaseService : ITestCaseService
    {
        private readonly ITestCaseRepository _testCaseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public TestCaseService(ITestCaseRepository testCaseRepository, IUserRepository userRepository, IProjectRepository projectRepository,  IMapper mapper)
        {
            _testCaseRepository = testCaseRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public IQueryable<TestCaseOutputDto> GetAll(Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            if (user.Role != UserRole.Admin)
                throw new UnauthorizedAccessException($"User does not have access to all projects.");

            return _testCaseRepository
                .GetAll()
                .ProjectTo<TestCaseOutputDto>(_mapper.ConfigurationProvider);
        }

        public IQueryable<TestCaseOutputDto> GetAllByProjectId(Guid projectId, Guid userId)
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

            return _testCaseRepository
                .GetAll()
                .Where(tc => tc.ProjectId == projectId)
                .ProjectTo<TestCaseOutputDto>(_mapper.ConfigurationProvider);
        }

        public async Task<TestCaseOutputDto> GetByIdAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testCase = await _testCaseRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testCase.Project.OwnerId != userId && !testCase.Project.ProjectUsers.Any(pu => pu.UserId == userId))
                throw new UnauthorizedAccessException($"User does not have access to test case with ID {id}.");

            return _mapper.Map<TestCaseOutputDto>(testCase);
        }

        public async Task<Guid> AddAsync(TestCaseCreateDto testCaseDto)
        {
            Validator.Validate(testCaseDto);

            var testCase = _mapper.Map<TestCase>(testCaseDto);

            if (testCaseDto.TagIds != null && testCaseDto.TagIds.Count != 0)
            {
                testCase.Tags = testCaseDto.TagIds
                    .Distinct()
                    .Select(id => new Tag
                    {
                        Id = id
                    })
                    .ToList();
            }

            if (testCaseDto.DefectIds != null && testCaseDto.DefectIds.Count != 0)
            {
                testCase.Defects = testCaseDto.DefectIds
                    .Distinct()
                    .Select(id => new Defect
                    {
                        Id = id
                    })
                    .ToList();
            }

            if (testCaseDto.StepIds != null && testCaseDto.StepIds.Count != 0)
            {
                testCase.Steps = testCaseDto.StepIds
                    .Distinct()
                    .Select(id => new TestStep
                    {
                        Id = id
                    })
                    .ToList();
            }

            testCase.Id = Guid.NewGuid();

            await _testCaseRepository.AddAsync(testCase);

            return testCase.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, TestCaseUpdateDto testCaseDto, Guid userId)
        {
            Validator.Validate(testCaseDto);

            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testCase = await _testCaseRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testCase.Project.OwnerId != userId && !testCase.Project.ProjectUsers.Any(pu => pu.UserId == userId))
                throw new UnauthorizedAccessException($"User does not have access to test case with ID {id}.");

            _mapper.Map(testCaseDto, testCase);

            if (testCaseDto.TagIds != null && testCaseDto.TagIds.Count != 0)
            {
                var currentTagIds = testCase.Tags.Select(t => t.Id).ToList();
                var incomingTagIds = testCaseDto.TagIds.Distinct().ToList();

                var tagsToRemove = testCase.Tags.Where(t => !incomingTagIds.Contains(t.Id)).ToList();
                var tagsToAdd = incomingTagIds
                    .Where(id => !currentTagIds.Contains(id))
                    .Select(id => new Tag { Id = id })
                    .ToList();

                foreach (var tag in tagsToRemove)
                    testCase.Tags.Remove(tag);

                testCase.Tags.AddRange(tagsToAdd);
            }

            if (testCaseDto.DefectIds != null && testCaseDto.DefectIds.Count != 0)
            {
                var currentDefectIds = testCase.Defects.Select(d => d.Id).ToList();
                var incomingDefectIds = testCaseDto.DefectIds.Distinct().ToList();

                var defectsToRemove = testCase.Defects.Where(d => !incomingDefectIds.Contains(d.Id)).ToList();
                var defectsToAdd = incomingDefectIds
                    .Where(id => !currentDefectIds.Contains(id))
                    .Select(id => new Defect { Id = id })
                    .ToList();

                foreach (var defect in defectsToRemove)
                    testCase.Defects.Remove(defect);

                testCase.Defects.AddRange(defectsToAdd);
            }

            if (testCaseDto.StepIds != null && testCaseDto.StepIds.Count != 0)
            {
                var currentStepsIds = testCase.Steps.Select(d => d.Id).ToList();
                var incomingStepsIds = testCaseDto.StepIds.Distinct().ToList();

                var stepsToRemove = testCase.Steps.Where(d => !incomingStepsIds.Contains(d.Id)).ToList();
                var stepsToAdd = incomingStepsIds
                    .Where(id => !currentStepsIds.Contains(id))
                    .Select(id => new TestStep { Id = id })
                    .ToList();

                foreach (var step in stepsToRemove)
                    testCase.Steps.Remove(step);

                testCase.Steps.AddRange(stepsToAdd);
            }

            return await _testCaseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testCase = await _testCaseRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testCase.Project.OwnerId != userId && !testCase.Project.ProjectUsers.Any(pu => pu.UserId == userId))
                throw new UnauthorizedAccessException($"User does not have access to test case with ID {id}.");

            return await _testCaseRepository.DeleteAsync(testCase);
        }
    }
}
