using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Services_Management_App_API.DAL;

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
}