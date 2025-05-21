using TmsSolution.Domain.Entities;

namespace TmsSolution.Infrastructure.Data.Repositories
{
    public interface IProjectRepository
    {
        IQueryable<Project> GetAll();
        Task<Project> GetByIdAsync(Guid id);
        Task<bool> AddAsync(Project project);
        Task<bool> UpdateAsync(Project project);
        Task<bool> DeleteAsync(Guid id);
    }
}
