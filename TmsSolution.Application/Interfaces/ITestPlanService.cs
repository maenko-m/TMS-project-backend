using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.TestPlan;

namespace TmsSolution.Application.Interfaces
{
    public interface ITestPlanService
    {
        IQueryable<TestPlanOutputDto> GetAll(Guid userId);
        IQueryable<TestPlanOutputDto> GetAllByProjectId(Guid projectId, Guid userId);
        Task<TestPlanOutputDto> GetByIdAsync(Guid id, Guid userId);
        Task<bool> AddAsync(TestPlanCreateDto testPlanDto);
        Task<bool> UpdateAsync(Guid id, TestPlanUpdateDto testPlanDto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
