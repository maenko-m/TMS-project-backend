using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Application.Dtos.TestSuite;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Infrastructure.Data.Interfaces;

namespace TmsSolution.Application.Services
{
    public class TestStepService : ITestStepService
    {
        private readonly ITestStepRepository _testStepRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public TestStepService(ITestStepRepository testStepRepository, IUserRepository userRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _testStepRepository = testStepRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public IQueryable<TestStepOutputDto> GetAll(Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            if (user.Role != UserRole.Admin)
                throw new UnauthorizedAccessException($"Current user does not have access to all test steps.");

            return _testStepRepository
                .GetAll()
                .Select(ts => new TestStepOutputDto
                {
                    Id = ts.Id,
                    TestCaseId = ts.TestCaseId,
                    Description = ts.Description,
                    ExpectedResult = ts.ExpectedResult,
                    Position = ts.Position,
                    CreatedAt = ts.CreatedAt,
                    UpdatedAt = ts.UpdatedAt
                });
        }

        public IQueryable<TestStepOutputDto> GetAllByProjectId(Guid projectId, Guid userId)
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

            return _testStepRepository
                .GetAll()
                .Where(ts => ts.TestCase.ProjectId == projectId)
                .Select(ts => new TestStepOutputDto
                {
                    Id = ts.Id,
                    TestCaseId = ts.TestCaseId,
                    Description = ts.Description,
                    ExpectedResult = ts.ExpectedResult,
                    Position = ts.Position,
                    CreatedAt = ts.CreatedAt,
                    UpdatedAt = ts.UpdatedAt
                });
        }

        public async Task<TestStepOutputDto> GetByIdAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testStep = await _testStepRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testStep.TestCase.Project.OwnerId != userId && !testStep.TestCase.Project.ProjectUsers.Any(pu => pu.UserId == userId))
                throw new UnauthorizedAccessException($"User does not have access to test step with ID {id}.");

            return _mapper.Map<TestStepOutputDto>(testStep);
        }

        public async Task<Guid> AddAsync(TestStepCreateDto testStepDto)
        {
            Validator.Validate(testStepDto);

            var testStep = _mapper.Map<TestStep>(testStepDto);

            testStep.Id = Guid.NewGuid();

            await _testStepRepository.AddAsync(testStep);

            return testStep.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, TestStepUpdateDto testStepDto, Guid userId)
        {
            Validator.Validate(testStepDto);

            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testStep = await _testStepRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testStep.TestCase.Project.OwnerId != userId && !testStep.TestCase.Project.ProjectUsers.Any(pu => pu.UserId == userId))
                throw new UnauthorizedAccessException($"User does not have access to test step with ID {id}.");

            _mapper.Map(testStepDto, testStep);

            return await _testStepRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testStep = await _testStepRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testStep.TestCase.Project.OwnerId != userId && !testStep.TestCase.Project.ProjectUsers.Any(pu => pu.UserId == userId))
                throw new UnauthorizedAccessException($"User does not have access to test step with ID {id}.");

            return await _testStepRepository.DeleteAsync(testStep);
        }
    }
}
