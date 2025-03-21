namespace StudentManagement.Application.DTOs.ExamDTO
{
    public class SubmitExamDTO
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public Dictionary<int, char> Answers { get; set; } = [];
    }
}
