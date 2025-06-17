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
    public class TestPlanRepository : RepositoryBase<TestPlan>, ITestPlanRepository
    {
        public TestPlanRepository(TmsDbContext context) : base(context) { }

        public IQueryable<TestPlan> GetAll()
        {
            return _context.TestPlans
                .Include(tp => tp.Project)
                .Include(tp => tp.TestPlanTestCases)
                    .ThenInclude(tpts => tpts.TestCase)
                .AsNoTracking();
        }

        public async Task<TestPlan> GetByIdAsync(Guid id)
        {
            return await _context.TestPlans
                .Include(tp => tp.Project)
                .Include(tp => tp.TestPlanTestCases)
                    .ThenInclude(tpts => tpts.TestCase)
                .FirstOrDefaultAsync(tp => tp.Id == id)
                ?? throw new Exception($"Test plan with ID {id} not found."); ; ;
        }
    }
}
