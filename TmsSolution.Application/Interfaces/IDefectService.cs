using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Defect;
using TmsSolution.Application.Dtos.Project;

namespace TmsSolution.Application.Interfaces
{
    public interface IDefectService
    {
        IQueryable<DefectOutputDto> GetAll(Guid userId);
        IQueryable<DefectOutputDto> GetAllByProjectId(Guid projectId, Guid userId);
        Task<DefectOutputDto> GetByIdAsync(Guid id, Guid userId);
        Task<bool> AddAsync(DefectCreateDto defectDto);
        Task<bool> UpdateAsync(Guid id, DefectUpdateDto defectDto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
