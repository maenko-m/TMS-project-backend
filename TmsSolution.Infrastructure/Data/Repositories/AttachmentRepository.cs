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
    public class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(TmsDbContext context) : base(context) { }

        public IQueryable<Attachment> GetAll()
        {
            return _context.Attachments
                .Include(a => a.Project)
                    .ThenInclude(p => p.ProjectUsers)
                .AsNoTracking();
        }

        public async Task<Attachment> GetByIdAsync(Guid id)
        {
            return await _context.Attachments
                .Include(a => a.Project)
                    .ThenInclude(p => p.ProjectUsers)
                .FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"Attachment with ID {id} not found."); ; ;
        }
    }
}
