using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.CourseDTO;
using StudentManagement.Application.DTOs.StudentDTO;
using StudentManagement.Application.Services.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructures;

namespace StudentManagement.Application.Services.Implementation
{
    public class CourseService(
        IApplicationDbContext context,
        IMapper mapper
    ) : ICourseService
    {
        public CourseViewModel? GetCourseById(int CourseId)
        {
            return context.Courses
                .Where(course => course.Id == CourseId)
                .Select(course => new CourseViewModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    StartDate = course.StartDate
                })
                .FirstOrDefault();
        }

        public List<CourseViewModel> GetCourses()
        {
            var Courses = context.Courses.ToList();
            //var result = Courses.Select(course => _mapper.Map<CourseViewModel>(course)).ToList();
            // var result = _mapper.ProjectTo<CourseViewModel>(query).ToList();
            var result = mapper.Map<List<CourseViewModel>>(context.Courses.ToList());

            return result;
        }

        public Course? CreateCourse(CourseCreateModel CourseCreateModel)
        {
            if (string.IsNullOrWhiteSpace(CourseCreateModel.CourseName))
            {
                return null;
            }

            var course = new Course
            {
                Name = CourseCreateModel.CourseName,
                StartDate = CourseCreateModel.StartDate
            };

            context.Courses.Add(course);
            context.SaveChanges();
            return course;
        }

        public Course? DeleteCourse(int Id)
        {
            var course = context.Courses.Find(Id);
            if (course == null)
            {
                return null;
            }

            context.Courses.Remove(course);
            context.SaveChanges();
            return course;
        }
        
        public Course? EditCourse(CourseUpdateModel CourseUpdateModel, int Id)
        {
            if (string.IsNullOrWhiteSpace(CourseUpdateModel.CourseName) || Id <= 0)
            {
                return null;
            }

            var course = context.Courses.Find(Id);
            if (course == null)
            {
                return null;
            }

            course.Name = CourseUpdateModel.CourseName;
            course.StartDate = CourseUpdateModel.StartDate;

            context.Courses.Update(course);
            context.SaveChanges();

            return course;
        }

        public List<CourseDetailsModel> GetCourseDetails()
        {
            var courses = context.Courses
                .Include(c => c.CourseStudents)
                .ThenInclude(cs => cs.Student)
                .AsNoTracking()
                .ToList();

            return mapper.Map<List<CourseDetailsModel>>(courses);
        }
    }
}