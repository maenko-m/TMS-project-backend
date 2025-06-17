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
    public class TestRunRepository : RepositoryBase<TestRun>, ITestRunRepository
    {
        public TestRunRepository(TmsDbContext context) : base(context) { }

        public void Attach(Defect defect)
        {
            _context.Attach(defect);
        }

        public IQueryable<TestRun> GetAll()
        {
            return _context.TestRuns
                .Include(tr => tr.Tags)
                .Include(tr => tr.TestRunTestCases)
                    .ThenInclude(trts => trts.TestCase)
                .Include(tr => tr.Defects)
                .Include(tr => tr.Project)
                    .ThenInclude(p => p.ProjectUsers)
                .AsNoTracking();
        }

        public async Task<TestRun> GetByIdAsync(Guid id)
        {
            return await _context.TestRuns
                .Include(tr => tr.Tags)
                .Include(tr => tr.TestRunTestCases)
                    .ThenInclude(trts => trts.TestCase)
                .Include(tr => tr.Defects)
                .Include(tr => tr.Project)
                    .ThenInclude(p => p.ProjectUsers)
                .FirstOrDefaultAsync(tr => tr.Id == id)
                ?? throw new Exception($"Test run with ID {id} not found.");
        }
    }
}
