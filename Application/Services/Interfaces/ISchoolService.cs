using StudentManagement.Application.DTOs.SchoolDTO;

namespace StudentManagement.Application.Services.Interfaces
{
    public interface ISchoolService
    {
        IEnumerable<SchoolViewModel> GetSchools(int? SchoolId);

        bool CreateSchool(SchoolCreateModel schoolCreateModel);

        bool UpdateSchool(SchoolUpdateModel schoolUpdateModel);

        bool DeleteSchool(int id);
    }
}