namespace WebApplication1.Dto.Course
{
     

    public record CourseListDto (Guid Id , string Title , decimal Price ,int StudentCount , bool IsActive);
    public record CreateCourseDto(string Title , string? Description ,decimal Price);
    public record UpdateCourseDto(  string? Description ,decimal Price , bool IsActive);

}
