using StudentManagement.Application.DTOs.SchoolDTO;
using StudentManagement.Application.Services.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructures;

namespace StudentManagement.Application.Services.Implementation
{
    public class SchoolService(IApplicationDbContext context) : ISchoolService
    {
        public IEnumerable<SchoolViewModel> GetSchools(int? SchoolId)
        {
            var Schools = context.Schools.AsQueryable();
            if (SchoolId.HasValue)
            {
                Schools = Schools.Where(school => school.Id == SchoolId);
            }
            return [.. Schools.Select(school => new SchoolViewModel
            {
                Name = school.Name,
                Address = school.Address
            })];
        }

        public bool CreateSchool(SchoolCreateModel schoolCreateModel)
        {
            if (string.IsNullOrWhiteSpace(schoolCreateModel.Name))
            {
                return false;
            }

            var school = new School
            {
                Name = schoolCreateModel.Name,
                Address = schoolCreateModel.Address
            };
            context.Schools.Add(school);
            context.SaveChanges();
            return true;
        }

        public bool UpdateSchool(SchoolUpdateModel schoolUpdateModel)
        {
            if (string.IsNullOrWhiteSpace(schoolUpdateModel.Name))
            {
                return false;
            }
            var school = context.Schools.FirstOrDefault(school => school.Id == schoolUpdateModel.Id);
            if (school == null)
            {
                return false;
            }
            school.Name = schoolUpdateModel.Name;
            school.Address = schoolUpdateModel.Address;
            context.SaveChanges();
            return true;
        }

        public bool DeleteSchool(int id)
        {
            var school = context.Schools.FirstOrDefault(school => school.Id == id);
            if (school == null)
            {
                return false;
            }
            context.Schools.Remove(school);
            context.SaveChanges();
            return true;
        }

        public SchoolViewModel GetSchoolDetails(int SchoolId)
        {
            var data = context.Schools.Find(SchoolId);
            if (data == null)
            {
                return null;
            }
            var students = data.Students;
            return new SchoolViewModel
            {
                Name = data.Name,
                Address = data.Address
            };
        }
    }
}