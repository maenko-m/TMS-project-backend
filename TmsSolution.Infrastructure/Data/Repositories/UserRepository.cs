using Microsoft.EntityFrameworkCore;
using TmsSolution.Domain.Entities;

namespace TmsSolution.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TmsDbContext _context;

        public UserRepository(TmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id)
                ?? throw new Exception($"User with ID {id} not found.");
        }
        public async Task<bool> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(p => p.Id == user.Id)
                ?? throw new Exception($"User with ID {user.Id} not found.");


            _context.Entry(existingUser).CurrentValues.SetValues(user);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(p => p.Id == id);

            if (user == null)
                return false;

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}
