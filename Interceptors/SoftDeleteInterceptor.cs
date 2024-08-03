﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CustomerManagement.Data.Base;

namespace CustomerManagement.Interceptors
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            IEnumerable<EntityEntry<ISoftDeletable>> entries =
                eventData
                    .Context
                    .ChangeTracker
                    .Entries<ISoftDeletable>()
                    .Where(e => e.State == EntityState.Deleted);

            foreach (EntityEntry<ISoftDeletable> softDeletableEntity in entries)
            {
                softDeletableEntity.State = EntityState.Modified;
                softDeletableEntity.Entity.IsDeleted = true;
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

    }
}
