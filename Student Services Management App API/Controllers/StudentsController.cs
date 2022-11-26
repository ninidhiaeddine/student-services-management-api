using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Services_Management_App_API.DAL;
using Student_Services_Management_App_API.Models;

namespace Student_Services_Management_App_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly DatabaseContext dbContext;

    public StudentsController(DatabaseContext context)
    {
        dbContext = context;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> GetAllStudents()
    {
        var students = DataAccessLayer.GetAllStudents(dbContext);

        if (students.Capacity > 0)
            return Ok(students);

        return NotFound();
    }

    [HttpGet("{studentId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> GetStudent(int studentId)
    {
        var student = DataAccessLayer.GetStudentById(dbContext, studentId);
        if (student != null)
            return Ok(student);

        return NotFound();
    }

    [HttpGet("me")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult> GetMe()
    {
        var me = GetCurrentUser();

        if (me != null)
            return Ok(me);

        return NotFound();
    }

    private Student? GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity == null)
            return null;

        var claims = identity.Claims;

        var student = new Student();
        student.FirstName = claims.FirstOrDefault(c => c.Type == "FirstName")?.Value;
        student.LastName = claims.FirstOrDefault(c => c.Type == "LastName")?.Value;
        student.Email = claims.FirstOrDefault(c => c.Type == "Email")?.Value;
        student.Gender = int.Parse(claims.FirstOrDefault(c => c.Type == "Gender")?.Value);
        student.IsDorms = int.Parse(claims.FirstOrDefault(c => c.Type == "IsDorms")?.Value);
        student.StudentId = int.Parse(claims.FirstOrDefault(c => c.Type == "StudentId")?.Value);

        return student;
    }
}