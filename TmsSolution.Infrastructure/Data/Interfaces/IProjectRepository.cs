using TmsSolution.Domain.Entities;

namespace TmsSolution.Infrastructure.Data.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        IQueryable<Project> GetAll();
        Task<Project> GetByIdAsync(Guid id);
    }
}
