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
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(TmsDbContext context) : base(context) { }

        public IQueryable<Tag> GetAll()
        {
            return _context.Tags
                .AsNoTracking();
        }

        public async Task<Tag> GetByIdAsync(Guid id)
        {
            return await _context.Tags.FindAsync(id)
                ?? throw new Exception($"Tag with ID {id} not found.");
        }
    }
}
