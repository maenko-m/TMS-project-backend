using TmsSolution.Application.Dtos.Project;
using TmsSolution.Application.Dtos.TestSuite;

namespace TmsSolution.Application.Interfaces
{
    public interface ITestSuiteService
    {
        IQueryable<TestSuiteOutputDto> GetAll(Guid userId);
        IQueryable<TestSuiteOutputDto> GetAllByProjectId(Guid projectId, Guid userId);
        Task<TestSuiteOutputDto> GetByIdAsync(Guid id, Guid userId);
        Task<bool> AddAsync(TestSuiteCreateDto testSuiteDto);
        Task<bool> UpdateAsync(Guid id, TestSuiteUpdateDto testSuiteDto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
