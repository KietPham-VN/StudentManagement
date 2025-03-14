using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.Application.DTOs.CourseDTO;
using StudentManagement.Application.DTOs.StudentCourseDTO;
using StudentManagement.Application.DTOs.StudentDTO;
using StudentManagement.Application.Services.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructures;

namespace StudentManagement.Application.Services.Implementation
{
    public class StudentService(IApplicationDbContext _context) : IStudentService
    {
        public List<CourseViewModel> GetStudentCourse(int StudentId)
        {
            var Data = _context.CourseStudents
                .Include(courseStudents => courseStudents.Course)
                .Where(courseStudents => courseStudents.StudentId == StudentId)
                .Select(courseStudents => new CourseViewModel
                {
                    Name = courseStudents.Course.Name,
                    AssginmentScore = courseStudents.AssignmentScore,
                    PracticalScore = courseStudents.PracticalScore,
                    FinalScore = courseStudents.FinalScore
                }).ToList();
            return Data;
        }

        public IEnumerable<StudentViewModel> GetStudents(int? SchoolId)
        {
            // query : select * From Student
            // Join School on Student.SchoolId = School.Id

            var students = _context.Students.Include(student => student.School)
                .AsQueryable();

            if (SchoolId.HasValue)
            {
                // query : select * From Student
                // Join School on Student.SchoolId = School.Id
                // Where School.Id = SchoolId
                students = students.Where(student => student.School.Id == SchoolId);
            }
            // IQueryable là 1 interface kế thừa từ IEnumerable
            // đại diện cho 1 tập hợp các phần tử có thể truy vấn

            // SELECT Student.Id,
            // Student.FristName + Student.LastName AS FullName,
            // Student.Age,
            // School.Name AS SchoolName
            // FROM Student
            // JOIN School ON Student.SchoolId = School.Id
            // WHERE School.Id = SchoolId (nếu schoolid bị null)

            // tới khúc này khi được .toList() thì query mới được chạy nè
            // nếu không thì nó chỉ là 1 câu lệnh truy vấn chưa được chạy
            // khúc này chắc chắc bị hỏi
            return [.. students.Select(student => new StudentViewModel
            {
                Id = student.Id,
                FullName = student.FirstName + " " + student.LastName,
                Age = student.Age,
                SchoolName = student.School!.Name!
            })];
        }

        public bool CreateStudent(StudentCreateModel student)
        {
            var data = new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                Address = student.Address,
                SchoolId = student.SchoolId
            };
            _context.Students.Add(data);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateStudent(StudentUpdateModel student)
        {
            var Student = _context.Students.Find(student.Id);
            if (Student == null)
            {
                return false;
            }

            if (Student.FirstName.IsNullOrEmpty() || Student.LastName.IsNullOrEmpty() || Student.Address.IsNullOrEmpty())
            {
                return false;
            }
            // Optimistic vs. Pessimistic locking
            // Optimistic locking: kiểm tra xem dữ liệu có bị thay đổi không
            // Pessimistic locking: khóa dữ liệu để tránh bị thay đổi (đánh đổi tốc độ để đổi lấy tính đúng đắng của data)
            Student.FirstName = student.FirstName;
            Student.LastName = student.LastName;
            Student.DateOfBirth = student.DateOfBirth;
            Student.Address = student.Address;
            Student.Balance = student.Balance;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            var student = _context.Students.Find(id);
            if (student == null)
            {
                return false;
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
            return true;
        }

        public StudentCourseViewModel GetStudentDetails(int StudentId)
        {
            var student = _context.Students
                .Include(student => student.CourseStudents)
                .ThenInclude(courseStudent => courseStudent.Course)
                .FirstOrDefault(student => student.Id == StudentId);

            return student == null ? new StudentCourseViewModel { } : new StudentCourseViewModel
            {
                StudentId = student.Id,
                StudentName = student.FirstName + " " + student.LastName,
                Courses = student.CourseStudents.Select(courseStudent => new CourseViewModel
                {
                    Id = courseStudent.Course.Id,
                    Name = courseStudent.Course.Name,
                    StartDate = courseStudent.Course.StartDate,
                }).ToList()
            };
        }

        public bool EnrollStudent(int StudentId, int CourseId)
        {
            var student = _context.Students.Find(StudentId);
            var course = _context.Courses.Find(CourseId);
            if (student == null || course == null)
            {
                return false;
            }

            var courseStudent = new CourseStudent
            {
                StudentId = StudentId,
                CourseId = CourseId
            };
            _context.CourseStudents.Add(courseStudent);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateScore(UpdateScoreModel updateScoreModel)
        {
            var courseStudent = _context.CourseStudents
                .Find(updateScoreModel.StudentId, updateScoreModel.CourseId);
            if (courseStudent == null)
            {
                return false;
            }
            courseStudent.AssignmentScore = (float)updateScoreModel.AssignmentScore;
            courseStudent.PracticalScore = (float)updateScoreModel.PracticalScore;
            courseStudent.FinalScore = (float)updateScoreModel.FinalScore;
            _context.SaveChanges();
            return true;
        }

        public List<CourseScoreViewModel> GetStudentCourses(int studentId)
        {
            var courses = _context.CourseStudents
                .Where(cs => cs.StudentId == studentId)
                .Join(
                    _context.Courses,
                    cs => cs.CourseId,
                    c => c.Id,
                    (cs, c) => new CourseScoreViewModel
                    {
                        CourseName = c.Name,
                        AssginmentScore = cs.AssignmentScore,
                        PracticalScore = cs.PracticalScore,
                        FinalScore = cs.FinalScore,
                        AverageScore = (cs.AssignmentScore + cs.PracticalScore + cs.FinalScore) / 3
                    })
                .ToList();

            return courses;
        }
    }
}