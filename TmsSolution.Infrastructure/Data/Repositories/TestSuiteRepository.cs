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
    public class TestSuiteRepository : RepositoryBase<TestSuite>, ITestSuiteRepository
    {
        public TestSuiteRepository(TmsDbContext context) : base(context) { }

        public IQueryable<TestSuite> GetAll()
        {
            return _context.TestSuites
                .Include(ts => ts.TestCases)
                .AsNoTracking();
        }

        public async Task<TestSuite> GetByIdAsync(Guid id)
        {
            return await _context.TestSuites
                .Include(ts => ts.TestCases)
                .FirstOrDefaultAsync(ts => ts.Id == id)
                ?? throw new Exception($"Test suite with ID {id} not found.");
        }
    }
}
