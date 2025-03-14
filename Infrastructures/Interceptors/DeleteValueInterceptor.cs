﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Entities.ExtraProperties;

namespace StudentManagement.Infrastructures.Interceptors
{
    public class DeleteValueInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var context = eventData.Context as ApplicationDbContext;
            var entries = context.ChangeTracker.Entries<Course>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDelete)
                {
                    entry.Entity.DeletedAt = DateTime.Now;
                    entry.Entity.DeletedBy = 1;
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }
            return base.SavingChanges(eventData, result);
        }
    }
}