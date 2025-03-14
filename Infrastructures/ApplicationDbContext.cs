using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructures.DatabaseMapping;
using StudentManagement.Infrastructures.Interceptors;
using ToDoApp.Infrastructure.Persistence.Configurations;
using TodoWeb.Infrastructure.interceptors;

namespace StudentManagement.Infrastructures
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public EntityEntry<T> Entity<T>(T entity) where T : class
        {
            return base.Entry(entity);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=KIETPA\\SQLEXPRESS;Database=StudentManagement;Trusted_Connection=True;TrustServerCertificate=True");
            // optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.AddInterceptors(
                new SqlQueriesLoggingInterceptor(),
                new AuditLogInterceptor(),
                new AddValueInterceptor(),
                new DeleteValueInterceptor()
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentMapping());
            modelBuilder.ApplyConfiguration(new SchoolMapping());
            modelBuilder.ApplyConfiguration(new CourseMapping());
            modelBuilder.ApplyConfiguration(new CourseStudentMapping());
            modelBuilder.Entity<Course>().HasQueryFilter(p => !p.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}