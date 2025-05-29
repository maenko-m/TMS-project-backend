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
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(TmsDbContext context) : base(context) { }

        public IQueryable<Project> GetAll()
        {
            return _context.Projects
                .Include(p => p.Owner)
                .Include(p => p.ProjectUsers)
                .Include(p => p.TestCases)
                .Include(p => p.Defects)
                .Include(p => p.Attachments)
                .AsNoTracking();
        }

        public async Task<Project> GetByIdAsync(Guid id)
        {
            return await _context.Projects
                .Include(p => p.Owner)
                .Include(p => p.ProjectUsers)
                .Include(p => p.TestCases)
                .Include(p => p.Defects)
                .Include(p => p.Attachments)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new Exception($"Project with ID {id} not found.");
        }
    }
}
