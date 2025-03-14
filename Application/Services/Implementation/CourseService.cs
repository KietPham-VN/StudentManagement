using AutoMapper;
using StudentManagement.Application.DTOs.CourseDTO;
using StudentManagement.Application.Services.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructures;

namespace StudentManagement.Application.Services.Implementation
{
    public class CourseService(
        IApplicationDbContext _context,
        IMapper _mapper
    ) : ICourseService
    {
        public CourseViewModel? GetCourseById(int CourseId)
        {
            return _context.Courses
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
            var Courses = _context.Courses.ToList();
            //var result = Courses.Select(course => _mapper.Map<CourseViewModel>(course)).ToList();
            // var result = _mapper.ProjectTo<CourseViewModel>(query).ToList();
            var result = _mapper.Map<List<CourseViewModel>>(_context.Courses.ToList());

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

            _context.Courses.Add(course);
            _context.SaveChanges();
            return course;
        }

        public Course? DeleteCourse(int Id)
        {
            var course = _context.Courses.Find(Id);
            if (course == null)
            {
                return null;
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();
            return course;
        }

        public Course? EditCourse(CourseUpdateModel CourseUpdateModel, int Id)
        {
            if (string.IsNullOrWhiteSpace(CourseUpdateModel.CourseName) || Id <= 0)
            {
                return null;
            }

            var course = _context.Courses.Find(Id);
            if (course == null)
            {
                return null;
            }

            course.Name = CourseUpdateModel.CourseName;
            course.StartDate = CourseUpdateModel.StartDate;

            _context.Courses.Update(course);
            _context.SaveChanges();

            return course;
        }
    }
}