namespace StudentManagement.Application.DTOs.StudentCourseDTO
{
    public class UpdateScoreModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double AssignmentScore { get; set; }
        public double PracticalScore { get; set; }
        public double FinalScore { get; set; }
    }
}