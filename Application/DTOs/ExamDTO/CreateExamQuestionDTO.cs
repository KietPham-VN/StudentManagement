namespace StudentManagement.Application.DTOs.ExamDTO
{
    public class CreateExamQuestionDTO
    {
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> Choices { get; set; }
    }
}
