namespace StudentManagement.Application.DTOs.StudentDTO
{
    public class StudentCreateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public int SchoolId { get; set; }
    }
}