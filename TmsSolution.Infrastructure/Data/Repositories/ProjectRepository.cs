using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Infrastructure.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TmsDbContext _context;

        public ProjectRepository(TmsDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .AsNoTracking()
                .Include(p => p.Owner)
                .Include(p => p.ProjectUsers)
                .ToListAsync();
        }

        public async Task<Project> GetByIdAsync(Guid id)
        {
            return await _context.Projects
                .AsNoTracking()
                .Include(p => p.Owner)
                .Include(p => p.ProjectUsers)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new Exception($"Project with ID {id} not found.");
        }

        public async Task<bool> AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Project project)
        {
            var existingProject = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == project.Id) 
                ?? throw new Exception($"Project with ID {project.Id} not found.");


            _context.Entry(existingProject).CurrentValues.SetValues(project);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return false;

            _context.Projects.Remove(project);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
