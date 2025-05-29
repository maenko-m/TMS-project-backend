using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Entities;
using TmsSolution.Infrastructure.Data.Interfaces;

namespace TmsSolution.Infrastructure.Data.Repositories
{
    public class DefectRepository : RepositoryBase<Defect>, IDefectRepository
    {
        public DefectRepository(TmsDbContext context) : base(context) { }

        public IQueryable<Defect> GetAll()
        {
            return _context.Defects
                .Include(d => d.Project)
                .AsNoTracking();
        }

        public async Task<Defect> GetByIdAsync(Guid id)
        {
            return await _context.Defects
                .Include(d => d.Project)
                .FirstOrDefaultAsync(d => d.Id == id)
                ?? throw new Exception($"Defect with ID {id} not found."); ;
        }
    }
}
