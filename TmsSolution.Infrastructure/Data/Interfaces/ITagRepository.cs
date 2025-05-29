using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Infrastructure.Data.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        IQueryable<Tag> GetAll();
        Task<Tag> GetByIdAsync(Guid id);
    }
}
