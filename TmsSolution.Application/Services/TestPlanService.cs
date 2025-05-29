using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Milestone;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Dtos.TestPlan;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Infrastructure.Data.Interfaces;
using TmsSolution.Infrastructure.Data.Repositories;

namespace TmsSolution.Application.Services
{
    public class TestPlanService : ITestPlanService
    {
        private readonly ITestPlanRepository _testPlanRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public TestPlanService(ITestPlanRepository testPlanRepository, IUserRepository userRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _testPlanRepository = testPlanRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public IQueryable<TestPlanOutputDto> GetAll(Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            if (user.Role != UserRole.Admin)
                throw new UnauthorizedAccessException($"Current user does not have access to all milestones.");

            return _testPlanRepository
                .GetAll()
                .ProjectTo<TestPlanOutputDto>(_mapper.ConfigurationProvider);
        }

        public IQueryable<TestPlanOutputDto> GetAllByProjectId(Guid projectId, Guid userId)
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

            return _testPlanRepository
                .GetAll()
                .Where(tp => tp.ProjectId == projectId)
                .ProjectTo<TestPlanOutputDto>(_mapper.ConfigurationProvider);
        }

        public async Task<TestPlanOutputDto> GetByIdAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
            ?? throw new Exception("User not found");

            var testPlan = await _testPlanRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testPlan.Project.OwnerId != userId && !testPlan.Project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"Current user does not have access to test plan with ID {id}.");

            return _mapper.Map<TestPlanOutputDto>(testPlan);
        }

        public async Task<bool> AddAsync(TestPlanCreateDto testPlanDto)
        {
            Validator.Validate(testPlanDto);

            var testPlan = _mapper.Map<TestPlan>(testPlanDto);

            if (testPlanDto.TestCaseIds != null && testPlanDto.TestCaseIds.Count > 0)
            {
                testPlan.TestPlanTestCases = testPlanDto.TestCaseIds
                    .Distinct()
                    .Select(caseId => new TestPlanTestCase
                    {
                        TestCaseId = caseId,
                        TestPlanId = testPlan.Id
                    })
                    .ToList();
            }

            return await _testPlanRepository.AddAsync(testPlan);
        }

        public async Task<bool> UpdateAsync(Guid id, TestPlanUpdateDto testPlanDto, Guid userId)
        {
            Validator.Validate(testPlanDto);

            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
            ?? throw new Exception("User not found");

            var testPlan = await _testPlanRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testPlan.Project.OwnerId != userId && !testPlan.Project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"Current user does not have access to test plan with ID {id}.");

            _mapper.Map(testPlanDto, testPlan);

            if (testPlanDto.TestCaseIds != null && testPlanDto.TestCaseIds.Count > 0)
            {
                var newIds = testPlanDto.TestCaseIds.Distinct().ToList();
                var existingIds = testPlan.TestPlanTestCases.Select(t => t.TestCaseId).ToList();

                var idsToRemove = existingIds.Except(newIds).ToList();
                var idsToAdd = newIds.Except(existingIds).ToList();

                testPlan.TestPlanTestCases.RemoveAll(x => idsToRemove.Contains(x.TestCaseId));

                foreach (var idToAdd in idsToAdd)
                {
                    testPlan.TestPlanTestCases.Add(new TestPlanTestCase
                    {
                        TestCaseId = idToAdd,
                        TestPlanId = testPlan.Id
                    });
                }
            }

            return await _testPlanRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
            ?? throw new Exception("User not found");

            var testPlan = await _testPlanRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testPlan.Project.OwnerId != userId && !testPlan.Project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"Current user does not have access to test plan with ID {id}.");

            return await _testPlanRepository.DeleteAsync(testPlan);
        }
    }
}
