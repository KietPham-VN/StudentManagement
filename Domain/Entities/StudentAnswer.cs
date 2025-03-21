namespace StudentManagement.Domain.Entities;

public class StudentAnswer
{
    public int Id { get; set; }
    public int ExamSubmissionId { get; set; }
    public int QuestionId { get; set; }
    public char SelectedAnswer { get; set; }
    public bool IsCorrect { get; set; }

    public ExamSubmission ExamSubmission { get; set; }
    public Question Question { get; set; }
}