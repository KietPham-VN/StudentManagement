using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructures;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TodoWeb.Infrastructure.interceptors
{
    public class AuditLogInterceptor : SaveChangesInterceptor
    {
        private List<EntityEntry> AddedEntities = [];

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var context = eventData.Context as ApplicationDbContext;

            var auditLogs = new List<AuditLog>();
            foreach (var entity in context.ChangeTracker.Entries())
            {
                if (entity.Entity is AuditLog)
                {
                    continue;
                }
                var log = new AuditLog
                {
                    EntityName = entity.Entity.GetType().Name,
                    //nếu entity là Student ==> EntityName là student
                    ActionTime = DateTime.Now,
                    Action = entity.State.ToString()
                };
                if (entity.State == EntityState.Added)
                {
                    AddedEntities.Add(entity);
                }
                if (entity.State == EntityState.Modified)
                {
                    log.OldValue = JsonSerializer.Serialize(entity.OriginalValues.ToObject());
                    log.NewValue = JsonSerializer.Serialize(entity.CurrentValues.ToObject());
                    auditLogs.Add(log);
                }
                if (entity.State == EntityState.Deleted)
                {
                    log.OldValue = JsonSerializer.Serialize(entity.OriginalValues.ToObject());
                    auditLogs.Add(log);
                }
            }
            if (auditLogs.Count != 0)
            {
                context.AuditLogs.AddRange(auditLogs);
            }
            //context.AddRange(auditLogs);// chỉ add vào memory và add đc nhiều tk
            return base.SavingChanges(eventData, result);
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            var context = eventData.Context as ApplicationDbContext;

            if (AddedEntities.Count != 0)
            {
                var auditLogs = AddedEntities.Select(entity => new AuditLog
                {
                    EntityName = entity.Entity.GetType().Name,
                    ActionTime = DateTime.Now,
                    Action = EntityState.Added.ToString(),
                    NewValue = JsonSerializer.Serialize(entity.CurrentValues.ToObject())
                });
                context.AuditLogs.AddRange(auditLogs);
                AddedEntities.Clear();
                context.SaveChanges();
            }
            return base.SavedChanges(eventData, result);
        }
    }
}