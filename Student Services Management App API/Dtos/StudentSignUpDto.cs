using System.ComponentModel.DataAnnotations;

namespace Student_Services_Management_App_API.Dtos;

public class StudentSignUpDto
{
    [Required(ErrorMessage = "Please enter a first name")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Please enter a last name")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Please enter an email")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Please enter a student ID")]
    public int StudentId { get; set; }

    [Required(ErrorMessage = "Please select a gender")]
    public int Gender { get; set; }

    [Required(ErrorMessage = "Please select dorms state")]
    public int IsDorms { get; set; }

    [Required(ErrorMessage = "Please enter a password")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}