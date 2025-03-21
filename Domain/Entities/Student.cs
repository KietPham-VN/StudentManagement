namespace StudentManagement.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        public int SchoolId { get; set; }
        public virtual School? School { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
        public ICollection<ExamSubmission> ExamSubmissions { get; set; }
    }
}