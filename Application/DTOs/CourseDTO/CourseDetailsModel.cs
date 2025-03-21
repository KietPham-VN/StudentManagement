using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.DTOs.CourseDTO;

public class CourseDetailsModel
{
    public string CourseName { get; set; }
    public DateTime StartDate { get; set; }
    public List<Student> Students { get; set; }
    
}