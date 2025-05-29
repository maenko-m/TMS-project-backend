using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.TestSuite;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Infrastructure.Data.Interfaces;

namespace TmsSolution.Application.Services
{
    public class TestSuiteService : ITestSuiteService
    {
        private readonly ITestSuiteRepository _testSuiteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public TestSuiteService(ITestSuiteRepository testSuiteRepository, IUserRepository userRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _testSuiteRepository = testSuiteRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public IQueryable<TestSuiteOutputDto> GetAll(Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            if (user.Role != UserRole.Admin)
                throw new UnauthorizedAccessException($"Current user does not have access to all test suites.");

            return _testSuiteRepository
                .GetAll()
                .Select(ts => new TestSuiteOutputDto
                {
                    Id = ts.Id,
                    ProjectId = ts.ProjectId,
                    Name = ts.Name,
                    Description = ts.Description,
                    Preconditions = ts.Preconditions,
                    CreatedAt = ts.CreatedAt,
                    UpdatedAt = ts.UpdatedAt,
                    TestCasesCount = ts.TestCases.Count,
                });
        }

        public IQueryable<TestSuiteOutputDto> GetAllByProjectId(Guid projectId, Guid userId)
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

            return _testSuiteRepository
                .GetAll()
                .Where(ts => ts.ProjectId == projectId)
                .Select(ts => new TestSuiteOutputDto
                {
                    Id = ts.Id,
                    ProjectId = ts.ProjectId,
                    Name = ts.Name,
                    Description = ts.Description,
                    Preconditions = ts.Preconditions,
                    CreatedAt = ts.CreatedAt,
                    UpdatedAt = ts.UpdatedAt,
                    TestCasesCount = ts.TestCases.Count,
                });
        }

        public async Task<TestSuiteOutputDto> GetByIdAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testSuite = await _testSuiteRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testSuite.Project.OwnerId != userId && !testSuite.Project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"User does not have access to test suite with ID {id}.");

            return _mapper.Map<TestSuiteOutputDto>(testSuite);
        }

        public async Task<bool> AddAsync(TestSuiteCreateDto testSuiteDto)
        {
            Validator.Validate(testSuiteDto);

            var testSuite = _mapper.Map<TestSuite>(testSuiteDto);

            return await _testSuiteRepository.AddAsync(testSuite);
        }

        public async Task<bool> UpdateAsync(Guid id, TestSuiteUpdateDto testSuiteDto, Guid userId)
        {
            Validator.Validate(testSuiteDto);

            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testSuite = await _testSuiteRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testSuite.Project.OwnerId != userId)
                throw new UnauthorizedAccessException($"User does not have access to test suite with ID {id}.");

            _mapper.Map(testSuiteDto, testSuite);

            return await _testSuiteRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testSuite = await _testSuiteRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testSuite.Project.OwnerId != userId)
                throw new UnauthorizedAccessException($"User does not have access to test suite with ID {id}.");

            return await _testSuiteRepository.DeleteAsync(testSuite);
        }
    }
}
