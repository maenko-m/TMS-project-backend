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
        private readonly ITagRepository _tagRepository;
        private readonly ITestStepRepository _testStepRepository;
        private readonly IDefectRepository _defectRepository;
        private readonly IMapper _mapper;

        public TestCaseService(ITestCaseRepository testCaseRepository, IUserRepository userRepository, IProjectRepository projectRepository, ITagRepository tagRepository, ITestStepRepository testStepRepository, IDefectRepository defectRepository, IMapper mapper)
        {
            _testCaseRepository = testCaseRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _tagRepository = tagRepository;
            _testStepRepository = testStepRepository;
            _defectRepository = defectRepository;
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

            if (user.Role != UserRole.Admin && project.OwnerId != userId)
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

            if (user.Role != UserRole.Admin && testCase.Project.OwnerId != userId)
                throw new UnauthorizedAccessException($"User does not have access to test case with ID {id}.");

            return _mapper.Map<TestCaseOutputDto>(testCase);
        }

        public async Task<bool> AddAsync(TestCaseCreateDto testCaseDto)
        {
            Validator.Validate(testCaseDto);

            var testCase = _mapper.Map<TestCase>(testCaseDto);

            return await _testCaseRepository.AddAsync(testCase);
        }

        public async Task<bool> UpdateAsync(Guid id, TestCaseUpdateDto testCaseDto, Guid userId)
        {
            Validator.Validate(testCaseDto);

            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testCase = await _testCaseRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testCase.Project.OwnerId != userId)
                throw new UnauthorizedAccessException($"User does not have access to test case with ID {id}.");

            _mapper.Map(testCaseDto, testCase);

            return await _testCaseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testCase = await _testCaseRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testCase.Project.OwnerId != userId)
                throw new UnauthorizedAccessException($"User does not have access to test case with ID {id}.");

            return await _testCaseRepository.DeleteAsync(testCase);
        }

        public async Task<bool> UpdateTestCaseStepsAsync(UpdateTestCaseStepsDto updateTestCaseStepsDto)
        {
            var testCase = await _testCaseRepository.GetByIdAsync(updateTestCaseStepsDto.TestCaseId);

            var currentIds = testCase.Steps.Select(s => s.Id).ToList();

            var toRemove = testCase.Steps.Where(s => !updateTestCaseStepsDto.StepIds.Contains(s.Id)).ToList();
            foreach (var step in toRemove)
                testCase.Steps.Remove(step);

            var toAdd = updateTestCaseStepsDto.StepIds.Except(currentIds).ToList();
            if (toAdd.Count != 0)
            {
                var stepsToAdd = await _testStepRepository
                    .GetAll()
                    .Where(s => toAdd.Contains(s.Id))
                    .ToListAsync();

                foreach (var step in stepsToAdd)
                    testCase.Steps.Add(step);
            }

            return await _testCaseRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateTestCaseTagsAsync(UpdateTestCaseTagsDto updateTestCaseTagsDto)
        {
            var testCase = await _testCaseRepository.GetByIdAsync(updateTestCaseTagsDto.TestCaseId);

            var currentIds = testCase.Tags.Select(t => t.Id).ToList();

            var toRemove = testCase.Tags.Where(t => !updateTestCaseTagsDto.TagIds.Contains(t.Id)).ToList();
            foreach (var tag in toRemove)
                testCase.Tags.Remove(tag);

            var toAdd = updateTestCaseTagsDto.TagIds.Except(currentIds).ToList();
            if (toAdd.Count != 0)
            {
                var tagsToAdd = await _tagRepository 
                    .GetAll()
                    .Where(t => toAdd.Contains(t.Id))
                    .ToListAsync();

                foreach (var tag in tagsToAdd)
                    testCase.Tags.Add(tag);
            }

            return await _testCaseRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateTestCaseDefectsAsync(UpdateTestCaseDefectsDto updateTestCaseDefectsDto)
        {
            var testCase = await _testCaseRepository.GetByIdAsync(updateTestCaseDefectsDto.TestCaseId);


            var currentIds = testCase.Defects.Select(d => d.Id).ToList();

            var toRemove = testCase.Defects.Where(d => !updateTestCaseDefectsDto.DefectIds.Contains(d.Id)).ToList();
            foreach (var defect in toRemove)
                testCase.Defects.Remove(defect);

            var toAdd = updateTestCaseDefectsDto.DefectIds.Except(currentIds).ToList();
            if (toAdd.Count != 0)
            {
                var defectsToAdd = await _defectRepository
                    .GetAll()
                    .Where(d => toAdd.Contains(d.Id))
                    .ToListAsync();

                foreach (var defect in defectsToAdd)
                    testCase.Defects.Add(defect);
            }

            return await _testCaseRepository.SaveChangesAsync();
        }
    }
}
