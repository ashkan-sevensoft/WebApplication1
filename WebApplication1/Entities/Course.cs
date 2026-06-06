using WebApplication1.Entities;

namespace WebApplication1.Entities
{
    public class Course : BaseEntity
    {

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        public List<Enrollment> Enrollments { get; set; } = [];
    }
   
}
