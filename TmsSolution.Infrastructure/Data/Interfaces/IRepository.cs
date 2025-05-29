using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Infrastructure.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<bool> SaveChangesAsync();
        Task<bool> DeleteAsync(T entity);
    }
}
