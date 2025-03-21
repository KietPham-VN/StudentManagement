namespace StudentManagement.Domain.Entities;

public class Question
{
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public string OptionA { get; set; }
    public string OptionB { get; set; }
    public string OptionC { get; set; }
    public string OptionD { get; set; }
    public char CorrectAnswer { get; set; }
    public DateTime CreatedAt { get; set; }
}