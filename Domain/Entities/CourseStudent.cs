namespace StudentManagement.Domain.Entities
{
    public class CourseStudent
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
        public float AssignmentScore { get; set; }
        public float PracticalScore { get; set; }
        public float FinalScore { get; set; }
    }
}