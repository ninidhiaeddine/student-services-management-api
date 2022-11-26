namespace Student_Services_Management_App_API.Dtos;

public class StudentSignUpDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int StudentId { get; set; }
    public int Gender { get; set; }
    public int IsDorms { get; set; }
    public string Password { get; set; } = string.Empty;
}