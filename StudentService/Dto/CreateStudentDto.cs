namespace StudentService.Dto;

public class CreateStudentDto(string fullName, string email, string phone)
{
    public  string FullName { get; set; } = fullName;
    public  string Email { get; set; } = email;
    public  string Phone { get; set; } = phone;
}
