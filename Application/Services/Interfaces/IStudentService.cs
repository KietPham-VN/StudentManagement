using StudentManagement.Application.DTOs.CourseDTO;
using StudentManagement.Application.DTOs.StudentCourseDTO;
using StudentManagement.Application.DTOs.StudentDTO;

namespace StudentManagement.Application.Services.Interfaces
{
    public interface IStudentService
    {
        List<CourseViewModel> GetStudentCourse(int StudentId);

        IEnumerable<StudentViewModel> GetStudents(int? SchoolId);

        bool CreateStudent(StudentCreateModel student);

        bool UpdateStudent(StudentUpdateModel student);

        bool DeleteStudent(int id);

        StudentCourseViewModel GetStudentDetails(int StudentId);

        bool EnrollStudent(int StudentId, int CourseId);

        bool UpdateScore(UpdateScoreModel updateScoreModel);

        List<CourseScoreViewModel> GetStudentCourses(int studentId);
    }
}