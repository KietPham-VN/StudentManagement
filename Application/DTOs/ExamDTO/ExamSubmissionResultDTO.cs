namespace StudentManagement.Application.DTOs.ExamDTO
{
    public class ExamSubmissionResultDTO
    {
        public int ExamSubmissionId { get; set; }
        public float Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public string Message { get; set; }
    }
}