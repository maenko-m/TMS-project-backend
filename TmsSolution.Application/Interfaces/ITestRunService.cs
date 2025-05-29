using TmsSolution.Application.Dtos.TestRun;

namespace TmsSolution.Application.Interfaces
{
    public interface ITestRunService
    {
        IQueryable<TestRunOutputDto> GetAll(Guid userId);
        IQueryable<TestRunOutputDto> GetAllByProjectId(Guid projectId, Guid userId);
        Task<TestRunOutputDto> GetByIdAsync(Guid id, Guid userId);
        Task<bool> AddAsync(TestRunCreateDto testRunDto);
        Task<bool> UpdateAsync(Guid id, TestRunUpdateDto testRunDto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
