namespace StudentManagement.Application.DTOs.ExamDTO
{
    public class CreateExamDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<CreateExamQuestionDTO> Questions { get; set; }
    }
}
