using WebApplication1.Entities;

namespace WebApplication1.Dto.Student
{
    public class StudentListDto
    {

        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
