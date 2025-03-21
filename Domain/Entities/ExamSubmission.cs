namespace StudentManagement.Domain.Entities;

public class ExamSubmission
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentId { get; set; }
    public float Score { get; set; }
    public DateTime SubmittedAt { get; set; }

    public Exam Exam { get; set; }
    public Student Student { get; set; }
    public ICollection<StudentAnswer> StudentAnswers { get; set; }
}