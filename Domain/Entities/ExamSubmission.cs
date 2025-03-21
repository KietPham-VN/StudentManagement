namespace StudentManagement.Domain.Entities;

public class ExamSubmission
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public int StudentId { get; set; }
    public float Score { get; set; }
    public DateTime SubmittedAt { get; set; }

    public Exam Exam { get; set; }
    public Student Student { get; set; }
    public ICollection<StudentAnswer> StudentAnswers { get; set; }
}