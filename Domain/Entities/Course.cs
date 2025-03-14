using StudentManagement.Domain.Entities.ExtraProperties;

namespace StudentManagement.Domain.Entities
{
    public class Course : ICreatedAt, ICreatedBy, IUpdatedAt, IUpdatedBy, ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}