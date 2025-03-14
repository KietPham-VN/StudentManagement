using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructures
{
    public interface IApplicationDbContext
    {
        DbSet<Student> Students { get; set; }
        DbSet<School> Schools { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<CourseStudent> CourseStudents { get; set; }
        DbSet<AuditLog> AuditLogs { get; set; }

        EntityEntry<T> Entity<T>(T entity) where T : class;

        int SaveChanges();
    }
}