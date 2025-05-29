using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Infrastructure.Data.Interfaces
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly TmsDbContext _context;

        protected RepositoryBase(TmsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _context.Set<T>().AddAsync(entity);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _context.Set<T>().Remove(entity);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
