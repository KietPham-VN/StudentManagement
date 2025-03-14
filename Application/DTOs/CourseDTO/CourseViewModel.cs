namespace StudentManagement.Application.DTOs.CourseDTO
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public float AssginmentScore { get; set; }
        public float PracticalScore { get; set; }
        public float FinalScore { get; set; }
    }
}