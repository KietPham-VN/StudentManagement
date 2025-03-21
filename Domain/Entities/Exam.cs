namespace StudentManagement.Domain.Entities;

public class Exam
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string ExamName { get; set; }
    public DateTime CreatedAt { get; set; }

    public Course Course { get; set; }
    public ICollection<ExamQuestion> ExamQuestions { get; set; }
}