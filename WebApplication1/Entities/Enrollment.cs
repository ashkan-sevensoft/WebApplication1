namespace WebApplication1.Entities
{
    public class Enrollment
    {
      
        public Guid StudentId { get; set; }
        public Student? Student { get; set; }

        public Guid CourseId { get; set; }
        public Course? Course { get; set; }

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public double? Score { get; set; }
    }

}
