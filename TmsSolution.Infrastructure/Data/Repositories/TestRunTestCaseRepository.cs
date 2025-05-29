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
    public class TestRunTestCaseRepository : RepositoryBase<TestRunTestCase>, ITestRunTestCaseRepository
    {
        public TestRunTestCaseRepository(TmsDbContext context) : base(context) { }

        public IQueryable<TestRunTestCase> GetAll()
        {
            return _context.TestRunTestCases
                .Include(trtc => trtc.TestCase)
                .Include(trtc => trtc.TestRun)
                    .ThenInclude(tr => tr.Project)
                        .ThenInclude(p => p.ProjectUsers)
                .AsNoTracking();
        }

        public async Task<TestRunTestCase> GetByIdAsync(Guid id)
        {
            return await _context.TestRunTestCases
                .Include(trtc => trtc.TestCase)
                .Include(trtc => trtc.TestRun)
                    .ThenInclude(tr => tr.Project)
                        .ThenInclude(p => p.ProjectUsers)
                .FirstOrDefaultAsync(trtc => trtc.Id == id)
                ?? throw new Exception($"Test run test case with ID {id} not found.");
        }
    }
}
