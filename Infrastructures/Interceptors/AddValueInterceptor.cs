using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StudentManagement.Domain.Entities;
using System.Linq;

namespace StudentManagement.Infrastructures.Interceptors
{
    public class AddValueInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            ApplicationDbContext? context = eventData.Context as ApplicationDbContext;

            var entries = context.ChangeTracker.Entries<Course>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = 1;
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.UpdatedBy = 1;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.UpdatedBy = 1;
                }
            }
            return base.SavingChanges(eventData, result);
        }
    }
}