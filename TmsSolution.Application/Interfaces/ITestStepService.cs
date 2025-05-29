using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.TestStep;
using TmsSolution.Application.Dtos.TestSuite;

namespace TmsSolution.Application.Interfaces
{
    public interface ITestStepService
    {
        IQueryable<TestStepOutputDto> GetAll(Guid userId);
        IQueryable<TestStepOutputDto> GetAllByProjectId(Guid projectId, Guid userId);
        Task<TestStepOutputDto> GetByIdAsync(Guid id, Guid userId);
        Task<bool> AddAsync(TestStepCreateDto testStepDto);
        Task<bool> UpdateAsync(Guid id, TestStepUpdateDto testStepDto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
