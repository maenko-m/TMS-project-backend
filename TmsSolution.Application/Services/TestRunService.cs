using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.TestRun;
using TmsSolution.Application.Dtos.TestRunTestCase;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Infrastructure.Data.Interfaces;
using TmsSolution.Infrastructure.Data.Repositories;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace TmsSolution.Application.Services
{
    public class TestRunService : ITestRunService
    {
        private readonly ITestRunRepository _testRunRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public TestRunService(ITestRunRepository testRunRepository, IUserRepository userRepository, IProjectRepository projectRepository, IDefectRepository defectRepository, IMapper mapper)
        {
            _testRunRepository = testRunRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public IQueryable<TestRunOutputDto> GetAll(Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            if (user.Role != UserRole.Admin)
                throw new UnauthorizedAccessException($"User does not have access to all projects.");

            return _testRunRepository
                .GetAll()
                .ProjectTo<TestRunOutputDto>(_mapper.ConfigurationProvider);
        }

        public IQueryable<TestRunOutputDto> GetAllByProjectId(Guid projectId, Guid userId)
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

            return _testRunRepository
                .GetAll()
                .Where(tr => tr.ProjectId == projectId)
                .ProjectTo<TestRunOutputDto>(_mapper.ConfigurationProvider);
        }

        public async Task<TestRunOutputDto> GetByIdAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testRun = await _testRunRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testRun.Project.OwnerId != userId && !testRun.Project.ProjectUsers.Any(pu => pu.UserId == userId))
                throw new UnauthorizedAccessException($"User does not have access to test run with ID {id}.");

            return _mapper.Map<TestRunOutputDto>(testRun);
        }

        public async Task<Guid> AddAsync(TestRunCreateDto testRunDto)
        {
            Validator.Validate(testRunDto);

            var testRun = _mapper.Map<TestRun>(testRunDto);

            if (testRunDto.TagIds != null && testRunDto.TagIds.Count != 0)
            {
                testRun.Tags = testRunDto.TagIds
                    .Distinct()
                    .Select(id => new Tag 
                    { 
                        Id = id 
                    })
                    .ToList();
            }

            if (testRunDto.TestRunTestCaseIds != null && testRunDto.TestRunTestCaseIds.Count != 0)
            {
                testRun.TestRunTestCases = testRunDto.TestRunTestCaseIds
                    .Distinct()
                    .Select(id => new TestRunTestCase
                    {
                        TestCaseId = id,
                        TestRunId = testRun.Id 
                    })
                    .ToList();
            }

            if (testRunDto.DefectIds != null && testRunDto.DefectIds.Count != 0)
            {
                foreach (var defectId in testRunDto.DefectIds)
                {
                    var defect = new Defect { Id = defectId };
                    _testRunRepository.Attach(defect); 
                    testRun.Defects.Add(defect);
                }
            }

            testRun.StartTime = DateTime.UtcNow;

            testRun.Id = Guid.NewGuid();

            await _testRunRepository.AddAsync(testRun);

            return testRun.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, TestRunUpdateDto testRunDto, Guid userId)
        {
            Validator.Validate(testRunDto);

            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testRun = await _testRunRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testRun.Project.OwnerId != userId && !testRun.Project.ProjectUsers.Any(pu => pu.UserId == userId))
                throw new UnauthorizedAccessException($"User does not have access to test run with ID {id}.");

            _mapper.Map(testRunDto, testRun);

            if (testRunDto.TagIds != null && testRunDto.TagIds.Count != 0)
            {
                var currentTagIds = testRun.Tags.Select(t => t.Id).ToList();
                var incomingTagIds = testRunDto.TagIds.Distinct().ToList();

                var tagsToRemove = testRun.Tags.Where(t => !incomingTagIds.Contains(t.Id)).ToList();
                var tagsToAdd = incomingTagIds
                    .Where(id => !currentTagIds.Contains(id))
                    .Select(id => new Tag { Id = id })
                    .ToList();

                foreach (var tag in tagsToRemove)
                    testRun.Tags.Remove(tag);

                testRun.Tags.AddRange(tagsToAdd);
            }

            if (testRunDto.TestRunTestCaseIds != null && testRunDto.TestRunTestCaseIds.Count != 0)
            {
                var currentTcIds = testRun.TestRunTestCases.Select(tc => tc.TestCaseId).ToList();
                var incomingTcIds = testRunDto.TestRunTestCaseIds.Distinct().ToList();

                var trtcToRemove = testRun.TestRunTestCases.Where(tc => !incomingTcIds.Contains(tc.TestCaseId)).ToList();
                var trtcToAdd = incomingTcIds
                    .Where(id => !currentTcIds.Contains(id))
                    .Select(id => new TestRunTestCase 
                    { 
                        TestCaseId = id, 
                        TestRunId = testRun.Id 
                    })
                    .ToList();

                foreach (var item in trtcToRemove)
                    testRun.TestRunTestCases.Remove(item);

                testRun.TestRunTestCases.AddRange(trtcToAdd);
            }

            if (testRunDto.DefectIds != null && testRunDto.DefectIds.Count != 0)
            {
                var currentDefectIds = testRun.Defects.Select(d => d.Id).ToList();
                var incomingDefectIds = testRunDto.DefectIds.Distinct().ToList();

                var defectsToRemove = testRun.Defects.Where(d => !incomingDefectIds.Contains(d.Id)).ToList();
                var defectsToAdd = incomingDefectIds
                    .Where(id => !currentDefectIds.Contains(id))
                    .Select(id => new Defect { Id = id })
                    .ToList();

                foreach (var defect in defectsToRemove)
                    testRun.Defects.Remove(defect);

                testRun.Defects.AddRange(defectsToAdd);
            }

            return await _testRunRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testRun = await _testRunRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testRun.Project.OwnerId != userId && !testRun.Project.ProjectUsers.Any(pu => pu.UserId == userId))
                throw new UnauthorizedAccessException($"User does not have access to test run with ID {id}.");

            return await _testRunRepository.DeleteAsync(testRun);
        }  
    }
}
