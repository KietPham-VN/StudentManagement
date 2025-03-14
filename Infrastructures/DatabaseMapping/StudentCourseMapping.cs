using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace ToDoApp.Infrastructure.Persistence.Configurations
{
    public class CourseStudentMapping : IEntityTypeConfiguration<CourseStudent>
    {
        public void Configure(EntityTypeBuilder<CourseStudent> builder)
        {
            builder.ToTable("CourseStudents");

            builder.HasKey(x => new { x.CourseId, x.StudentId });

            builder
                .HasOne(x => x.Student)
                .WithMany(x => x.CourseStudents)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Course)
                .WithMany(x => x.CourseStudents)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}