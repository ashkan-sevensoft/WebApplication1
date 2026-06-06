namespace WebApplication1.Dto.Student
{
    
    public record StudentListDtoRec(Guid Id , string FullName , string Email ,int Age);

    public record CreateStudentDto(string FullName, string Email , int Age);
    public record UpdateStudentDto(string FullName, string Email , int Age);
}
