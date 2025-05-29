using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.TestRun;
using TmsSolution.Application.Dtos.TestRunTestCase;

namespace TmsSolution.Application.Interfaces
{
    public interface ITestRunTestCaseService
    {
        IQueryable<TestRunTestCaseOutputDto> GetAllByTestRunId(Guid testRunId, Guid userId);
        Task<TestRunTestCaseOutputDto> GetByIdAsync(Guid id, Guid userId);
        Task<bool> AddAsync(TestRunTestCaseCreateDto testRunDto);
        Task<bool> UpdateAsync(Guid id, TestRunTestCaseUpdateDto testRunDto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
