namespace WebApplication1.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }= Guid.NewGuid();

        public bool IsDelted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
