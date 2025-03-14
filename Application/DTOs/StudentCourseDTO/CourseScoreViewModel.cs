namespace StudentManagement.Application.DTOs.StudentCourseDTO
{
    public class CourseScoreViewModel
    {
        public string CourseName { get; set; }
        public double AssginmentScore { get; set; }
        public double PracticalScore { get; set; }
        public double FinalScore { get; set; }
        public double AverageScore { get; set; }
    }
}