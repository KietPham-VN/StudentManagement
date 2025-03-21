namespace StudentManagement.Domain.Entities;

public class Exam
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string ExamName { get; set; }
    public DateTime CreatedAt { get; set; }

    public Course Course { get; set; }
    public ICollection<ExamQuestion> ExamQuestions { get; set; }
}