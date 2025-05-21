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
        IQueryable<ProjectOutputDto> GetAll();
        Task<ProjectOutputDto> GetByIdAsync(Guid id);
        Task<bool> AddAsync(ProjectCreateDto projectDto);
        Task<bool> UpdateAsync(Guid id, ProjectUpdateDto projectDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
