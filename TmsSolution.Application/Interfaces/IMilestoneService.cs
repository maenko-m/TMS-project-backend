using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.Milestone;

namespace TmsSolution.Application.Interfaces
{
    public interface IMilestoneService
    {
        IQueryable<MilestoneOutputDto> GetAll(Guid userId);
        IQueryable<MilestoneOutputDto> GetAllByProjectId(Guid projectId, Guid userId);
        Task<MilestoneOutputDto> GetByIdAsync(Guid id, Guid userId);
        Task<bool> AddAsync(MilestoneCreateDto milestoneDto);
        Task<bool> UpdateAsync(Guid id, MilestoneUpdateDto milestoneDto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
