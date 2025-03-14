namespace StudentManagement.Application.DTOs.CourseDTO
{
    public class CourseUpdateModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
    }
}