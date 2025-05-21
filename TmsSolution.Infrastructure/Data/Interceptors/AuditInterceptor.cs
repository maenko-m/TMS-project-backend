using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmsSolution.Domain.Interfaces;

namespace TmsSolution.Infrastructure.Data.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateTimestamps(eventData);
            return result;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            UpdateTimestamps(eventData);
            return new ValueTask<InterceptionResult<int>>(result);
        }

        private void UpdateTimestamps(DbContextEventData eventData)
        {
            if (eventData == null)
                return;

            // Обработка IAuditable сущностей
            var auditableEntries = eventData.Context.ChangeTracker.Entries<IAuditable>();
            foreach (var entry in auditableEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            // Обработка ICreatable сущностей
            var creatableEntries = eventData.Context.ChangeTracker.Entries<ICreatable>();
            foreach (var entry in creatableEntries)
            {
                if (entry.State == EntityState.Added && entry.Entity is not IAuditable)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
