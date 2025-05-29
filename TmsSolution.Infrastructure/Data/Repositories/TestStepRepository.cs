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
    public class TestStepRepository : RepositoryBase<TestStep>, ITestStepRepository
    {
        public TestStepRepository(TmsDbContext context) : base(context) { }

        public IQueryable<TestStep> GetAll()
        {
            return _context.TestSteps
                .AsNoTracking();
        }

        public async Task<TestStep> GetByIdAsync(Guid id)
        {
            return await _context.TestSteps
                .Include(ts => ts.TestCase)
                    .ThenInclude(ts => ts.Project)
                .FirstOrDefaultAsync(ts => ts.Id == id)
                ?? throw new Exception($"Test step with ID {id} not found."); ;
        }
    }
}
