using Microsoft.EntityFrameworkCore;
using TmsSolution.Domain.Entities;
using TmsSolution.Infrastructure.Data.Interfaces;

namespace TmsSolution.Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(TmsDbContext context) : base(context) { }

        public IQueryable<User> GetAll()
        {
            return _context.Users.AsNoTracking();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id)
                ?? throw new Exception($"User with ID {id} not found.");
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new Exception($"User with email {email} not found.");
        }
    }
}
