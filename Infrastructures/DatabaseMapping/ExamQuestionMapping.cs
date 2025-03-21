using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructures.DatabaseMapping;

public class ExamQuestionMapping : IEntityTypeConfiguration<ExamQuestion>
{
    public void Configure(EntityTypeBuilder<ExamQuestion> builder)
    {
        builder.HasOne(e => e.Exam)
            .WithMany(e => e.ExamQuestions)
            .HasForeignKey(e => e.ExamId);

        builder.HasOne(e => e.Question)
            .WithMany()
            .HasForeignKey(e => e.QuestionId);
    }
}