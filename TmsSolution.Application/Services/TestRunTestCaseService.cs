using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.TestPlan;
using TmsSolution.Application.Dtos.TestRunTestCase;
using TmsSolution.Application.Interfaces;
using TmsSolution.Application.Utilities;
using TmsSolution.Domain.Entities;
using TmsSolution.Domain.Enums;
using TmsSolution.Infrastructure.Data.Interfaces;
using TmsSolution.Infrastructure.Data.Repositories;

namespace TmsSolution.Application.Services
{
    public class TestRunTestCaseService : ITestRunTestCaseService
    {
        private readonly ITestRunTestCaseRepository _testRunTestCaseRepository;
        private readonly ITestRunRepository _testRunRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TestRunTestCaseService(ITestRunTestCaseRepository testRunTestCaseRepository, ITestRunRepository testRunRepository, IUserRepository userRepository, IMapper mapper)
        {
            _testRunTestCaseRepository = testRunTestCaseRepository;
            _testRunRepository = testRunRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IQueryable<TestRunTestCaseOutputDto> GetAllByTestRunId(Guid testRunId, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testRun = _testRunRepository
                .GetAll()
                .FirstOrDefault(tr => tr.Id == testRunId)
                ?? throw new Exception("Test run not found");

            if (user.Role != UserRole.Admin && testRun.Project.OwnerId != userId && !testRun.Project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"User does not have access to test run with ID {testRunId}.");

            return _testRunTestCaseRepository
                .GetAll()
                .Where(trts => trts.TestRunId == testRunId)
                .ProjectTo<TestRunTestCaseOutputDto>(_mapper.ConfigurationProvider);
        }

        public async Task<TestRunTestCaseOutputDto> GetByIdAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testRunTestCase = await _testRunTestCaseRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testRunTestCase.TestRun.Project.OwnerId != userId && !testRunTestCase.TestRun.Project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"User does not have access to test run test case with ID {id}.");

            return _mapper.Map<TestRunTestCaseOutputDto>(testRunTestCase);
        }

        public async Task<bool> AddAsync(TestRunTestCaseCreateDto testRunDto)
        {
            Validator.Validate(testRunDto);

            var testRunTestCase = _mapper.Map<TestRunTestCase>(testRunDto);

            return await _testRunTestCaseRepository.AddAsync(testRunTestCase);
        }

        public async Task<bool> UpdateAsync(Guid id, TestRunTestCaseUpdateDto testRunDto, Guid userId)
        {
            Validator.Validate(testRunDto);

            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testRunTestCase = await _testRunTestCaseRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testRunTestCase.TestRun.Project.OwnerId != userId && !testRunTestCase.TestRun.Project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"User does not have access to test run test case with ID {id}.");

            _mapper.Map(testRunDto, testRunTestCase);

            return await _testRunTestCaseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Id == userId)
                ?? throw new Exception("User not found");

            var testRunTestCase = await _testRunTestCaseRepository.GetByIdAsync(id);

            if (user.Role != UserRole.Admin && testRunTestCase.TestRun.Project.OwnerId != userId && !testRunTestCase.TestRun.Project.ProjectUsers.Any(pu => pu.Id == userId))
                throw new UnauthorizedAccessException($"User does not have access to test run test case with ID {id}.");

            return await _testRunTestCaseRepository.DeleteAsync(testRunTestCase);
        }

    }
}
