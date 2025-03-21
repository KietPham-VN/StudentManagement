using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructures.DatabaseMapping;

public class ExamSubmissionMapping : IEntityTypeConfiguration<ExamSubmission>
{
    public void Configure(EntityTypeBuilder<ExamSubmission> builder)
    {
        builder.HasOne(e => e.Student)
            .WithMany(s => s.ExamSubmissions)
            .HasForeignKey(e => e.StudentId);
    }
}