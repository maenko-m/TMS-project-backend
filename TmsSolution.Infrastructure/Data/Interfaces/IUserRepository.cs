using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Infrastructure.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> GetAll();
        Task<User> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
    }
}
