namespace WebApplication1.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Enrollment> Enrollments { get; set; } = [];
    }
}