using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.CourseDTO;
using StudentManagement.Application.DTOs.StudentCourseDTO;
using StudentManagement.Application.DTOs.StudentDTO;
using StudentManagement.Application.Services.Interfaces;

namespace StudentManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController(IStudentService studentService) : ControllerBase
    {
        [HttpGet("{id:int}")]
        public StudentCourseViewModel GetStudentDetails(int id)
        {
            return studentService.GetStudentDetails(id);
        }

        [HttpGet]
        public IEnumerable<StudentViewModel> GetStudents(int? SchoolId)
        {
            return studentService.GetStudents(SchoolId);
        }

        [HttpGet("courses/{Id}")]
        [ProducesResponseType(typeof(List<CourseViewModel>), 200)]
        public IActionResult GetStudentCourse(int Id)
        {
            var courses = studentService.GetStudentCourse(Id);
            return Ok(courses);
        }

        [HttpPost]
        public bool CreateStudent(StudentCreateModel student)
        {
            return studentService.CreateStudent(student);
        }

        [HttpPut]
        public bool UpdateStudent(StudentUpdateModel student)
        {
            return studentService.UpdateStudent(student);
        }

        [HttpDelete]
        public bool DeleteStudent(int id)
        {
            return studentService.DeleteStudent(id);
        }

        [HttpPut("enroll")]
        public bool EnrollStudent(int StudentId, int CourseId)
        {
            return studentService.EnrollStudent(StudentId, CourseId);
        }

        [HttpPut("update-score")]
        public bool UpdateScore(UpdateScoreModel updateScoreModel)
        {
            return studentService.UpdateScore(updateScoreModel);
        }

        [HttpGet("GPA")]
        public List<CourseScoreViewModel> GetGPA(int StudentId)
        {
            return studentService.GetStudentCourses(StudentId);
        }
    }
}