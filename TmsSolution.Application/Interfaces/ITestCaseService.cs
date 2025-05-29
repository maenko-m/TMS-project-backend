using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.TestCase;
using TmsSolution.Application.Dtos.TestSuite;

namespace TmsSolution.Application.Interfaces
{
    public interface ITestCaseService
    {
        IQueryable<TestCaseOutputDto> GetAll(Guid userId);
        IQueryable<TestCaseOutputDto> GetAllByProjectId(Guid projectId, Guid userId);
        Task<TestCaseOutputDto> GetByIdAsync(Guid id, Guid userId);
        Task<bool> AddAsync(TestCaseCreateDto testCaseDto);
        Task<bool> UpdateAsync(Guid id, TestCaseUpdateDto testCaseDto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
