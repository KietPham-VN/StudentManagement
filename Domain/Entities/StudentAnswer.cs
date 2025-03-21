namespace StudentManagement.Domain.Entities;

public class StudentAnswer
{
    public Guid Id { get; set; }
    public Guid ExamSubmissionId { get; set; }
    public Guid QuestionId { get; set; }
    public char SelectedAnswer { get; set; }
    public bool IsCorrect { get; set; }

    public ExamSubmission ExamSubmission { get; set; }
    public Question Question { get; set; }
}