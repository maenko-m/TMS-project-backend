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
    public class TestCaseRepository : RepositoryBase<TestCase>, ITestCaseRepository
    {
        public TestCaseRepository(TmsDbContext context) : base(context) { }

        public IQueryable<TestCase> GetAll()
        {
            return _context.TestCases
                .Include(tc => tc.Project)
                .Include(tc => tc.Tags)
                .Include(tc => tc.Steps)
                .Include(tc => tc.Defects)
                .AsNoTracking();
        }

        public async Task<TestCase> GetByIdAsync(Guid id)
        {
            return await _context.TestCases
                .Include(tc => tc.Project)
                .Include(tc => tc.Tags)
                .Include(tc => tc.Steps)
                .Include(tc => tc.Defects)
                .FirstOrDefaultAsync(tc => tc.Id == id)
                ?? throw new Exception($"Test case with ID {id} not found.");
        }
    }
}
