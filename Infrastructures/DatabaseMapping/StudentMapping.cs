using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace ToDoApp.Infrastructure.Persistence.Configurations
{
    public class StudentMapping : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.FirstName)
                .HasColumnName("Surname")
                .HasMaxLength(255);

            builder
                .Property(x => x.LastName)
                .HasMaxLength(255);

            builder
                .Property(x => x.Balance)
                .IsConcurrencyToken();

            builder
                .Property(x => x.DateOfBirth)
                .IsRequired();
            builder.Property(x => x.Address);

            builder
                .Property(x => x.Age)
                .HasComputedColumnSql("DATEDIFF(YEAR, DateOfBirth, GETDATE())");

            builder
                .HasOne(x => x.School)
                .WithMany(s => s.Students)
                .HasForeignKey(x => x.SchoolId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.CourseStudents)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}