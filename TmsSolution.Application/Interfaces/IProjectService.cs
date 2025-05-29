using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Application.Dtos.Project;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Application.Interfaces
{
    public interface IProjectService
    {
        IQueryable<ProjectOutputDto> GetAll(Guid userId);
        Task<ProjectOutputDto> GetByIdAsync(Guid id, Guid userId);
        Task<bool> AddAsync(ProjectCreateDto projectDto);
        Task<bool> UpdateAsync(Guid id, ProjectUpdateDto projectDto, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
