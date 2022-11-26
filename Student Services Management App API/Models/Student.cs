using System.ComponentModel.DataAnnotations;

namespace Student_Services_Management_App_API.Models;

public class Student
{
    [Key] public int PK_Student { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int StudentId { get; set; }
    public int Gender { get; set; }
    public int IsDorms { get; set; }
    public string HashedPassword { get; set; } = string.Empty;
}