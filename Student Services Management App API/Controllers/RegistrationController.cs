using Microsoft.AspNetCore.Mvc;
using Student_Services_Management_App_API.DAL;
using Student_Services_Management_App_API.Dtos;
using Student_Services_Management_App_API.Models;

namespace Student_Services_Management_App_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly DatabaseContext dbContext;

    public RegistrationController(DatabaseContext context)
    {
        dbContext = context;
    }

    [HttpPost("students")]
    public async Task<ActionResult<Student>> SignUpAsStudent([FromBody] StudentSignUpDto student)
    {
        DataAccessLayer.AddStudent(
            dbContext,
            student.FirstName,
            student.LastName,
            student.Email,
            student.StudentId,
            student.Gender,
            student.IsDorms,
            BCrypt.Net.BCrypt.HashPassword(student.Password)
        );

        return Ok();
    }

    [HttpPost("admins")]
    public async Task<ActionResult<Student>> SignUpAsAdmin([FromBody] AdminSignUpDto admin)
    {
        DataAccessLayer.AddAdmin(
            dbContext,
            admin.FirstName,
            admin.LastName,
            admin.Email,
            BCrypt.Net.BCrypt.HashPassword(admin.Password)
        );

        return Ok();
    }
}