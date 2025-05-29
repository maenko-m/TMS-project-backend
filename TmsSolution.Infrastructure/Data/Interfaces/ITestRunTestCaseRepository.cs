using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Infrastructure.Data.Interfaces
{
    public interface ITestRunTestCaseRepository : IRepository<TestRunTestCase>
    {
        IQueryable<TestRunTestCase> GetAll();
        Task<TestRunTestCase> GetByIdAsync(Guid id);
    }
}
