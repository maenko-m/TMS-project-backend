using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Infrastructure.Data.Interfaces
{
    public interface ITestRunRepository : IRepository<TestRun>
    {
        void Attach(Defect defect);
        IQueryable<TestRun> GetAll();
        Task<TestRun> GetByIdAsync(Guid id);
    }
}
