using StudentManagement.Application.DTOs.CourseDTO;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Services.Interfaces
{
    public interface ICourseService
    {
        CourseViewModel? GetCourseById(int CourseId);

        List<CourseViewModel> GetCourses();

        Course? CreateCourse(CourseCreateModel CourseCreateModel);

        Course? EditCourse(CourseUpdateModel CourseUpdateModel, int Id);

        Course? DeleteCourse(int Id);
        List<CourseDetailsModel> GetCourseDetails();
    }
}