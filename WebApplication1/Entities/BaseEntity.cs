namespace WebApplication1.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }= Guid.NewGuid();
    }
}
