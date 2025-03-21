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
        DbSet<Question> Questions { get; set; }
        DbSet<Exam> Exams { get; set; }
        DbSet<ExamQuestion> ExamQuestions { get; set; }
        DbSet<ExamSubmission> ExamSubmissions { get; set; }
        DbSet<StudentAnswer> StudentAnswers { get; set; }


        EntityEntry<T> Entity<T>(T entity) where T : class;

        int SaveChanges();
    }
}