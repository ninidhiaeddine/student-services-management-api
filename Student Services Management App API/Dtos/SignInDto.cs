using System.ComponentModel.DataAnnotations;

namespace Student_Services_Management_App_API.Dtos;

public class SignInDto
{
    [Required(ErrorMessage = "Please enter an email.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please enter a password.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}