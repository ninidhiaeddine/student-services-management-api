using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Student_Services_Management_App_API.Dtos;
using Student_Services_Management_App_API.Models;

namespace Student_Services_Management_App_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("signup/students")]
    public async Task<ActionResult<Student>> SignUpAsStudent([FromBody] StudentDto student)
    {
        Console.WriteLine(student.FirstName);
        Console.WriteLine(student.LastName);
        Console.WriteLine(student.Email);
        Console.WriteLine(student.StudentId);
        Console.WriteLine(student.Gender);
        Console.WriteLine(student.IsDorms);
        Console.WriteLine(student.Password);

        Student.AddStudentDb(
            student.FirstName,
            student.LastName,
            student.Email,
            student.StudentId,
            student.Gender,
            student.IsDorms,
            HashPassword(student.Password));

        return Ok();
    }

    [HttpPost("signup/admins")]
    public async Task<ActionResult<Student>> SignUpAsAdmin()
    {
        return Ok();
    }

    [HttpPost("signin/students")]
    public async Task<ActionResult<Student>> SignInAsStudent()
    {
        return Ok();
    }

    [HttpPost("signin/admins")]
    public async Task<ActionResult<Student>> SignInAsAdmin([FromBody] Student student)
    {
        return Ok();
    }

    private static string HashPassword(string password)
    {
        // Generate a 128-bit salt using a sequence of
        // cryptographically strong random bytes.
        var salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password!,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100000,
            256 / 8));

        return hashed;
    }
}