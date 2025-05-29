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
    public class MilestoneRepository : RepositoryBase<Milestone>, IMilestoneRepository
    {
        public MilestoneRepository(TmsDbContext context) : base(context) { }

        public IQueryable<Milestone> GetAll()
        {
            return _context.Milestones
                .Include(m => m.Project)
                    .ThenInclude(m => m.ProjectUsers)
                .Include(m => m.TestRuns)
                .AsNoTracking();
        }

        public async Task<Milestone> GetByIdAsync(Guid id)
        {
            return await _context.Milestones
                .Include(m => m.Project)
                    .ThenInclude(m => m.ProjectUsers)
                .Include(m => m.TestRuns)
                .FirstOrDefaultAsync(m => m.Id == id)
                ?? throw new Exception($"Milestone with ID {id} not found.");
        }
    }
}
