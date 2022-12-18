using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Student_Services_Management_App_API.DAL;
using Student_Services_Management_App_API.Dtos;
using Student_Services_Management_App_API.Models;

namespace Student_Services_Management_App_API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly DatabaseContext dbContext;

    public AuthController(DatabaseContext context)
    {
        dbContext = context;
    }

    [HttpPost("students")]
    public async Task<ActionResult<Student>> SignInStudent([FromBody] SignInDto signIn)
    {
        var student = AuthenticateStudent(signIn);

        if (student == null)
            return NotFound();

        var token = GenerateToken(student);
        return Ok(token);
    }

    [HttpPost("admins")]
    public async Task<ActionResult<Student>> SignInAdmin([FromBody] SignInDto signIn)
    {
        var admin = AuthenticateAdmin(signIn);

        if (admin == null)
            return NotFound();

        var token = GenerateToken(admin);

        return Ok(token);
    }

    [HttpGet("students/me")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult> GetStudentMe()
    {
        var me = GetCurrentStudent();

        if (me != null)
            return Ok(me);

        return NotFound();
    }

    [HttpGet("admins/me")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> GetAdminMe()
    {
        var me = GetCurrentAdmin();

        if (me != null)
            return Ok(me);

        return NotFound();
    }

    private Admin? GetCurrentAdmin()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity == null)
            return null;

        var claims = identity.Claims;


        int PK_Admin;
        int.TryParse(claims.FirstOrDefault(c => c.Type == "PK_Admin")?.Value, out PK_Admin);

        var admin = new Admin();
        admin.PK_Admin = PK_Admin;
        admin.FirstName = claims.FirstOrDefault(c => c.Type == "FirstName")?.Value;
        admin.LastName = claims.FirstOrDefault(c => c.Type == "LastName")?.Value;
        admin.Email = claims.FirstOrDefault(c => c.Type == "Email")?.Value;

        return admin;
    }

    private Student? GetCurrentStudent()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity == null)
            return null;

        var claims = identity.Claims;

        int PK_Student, gender, isDorms, studentId;
        int.TryParse(claims.FirstOrDefault(c => c.Type == "PK_Student")?.Value, out PK_Student);
        int.TryParse(claims.FirstOrDefault(c => c.Type == "Gender")?.Value, out gender);
        int.TryParse(claims.FirstOrDefault(c => c.Type == "IsDorms")?.Value, out isDorms);
        int.TryParse(claims.FirstOrDefault(c => c.Type == "StudentId")?.Value, out studentId);

        var student = new Student();

        student.PK_Student = PK_Student;
        student.FirstName = claims.FirstOrDefault(c => c.Type == "FirstName")?.Value;
        student.LastName = claims.FirstOrDefault(c => c.Type == "LastName")?.Value;
        student.Email = claims.FirstOrDefault(c => c.Type == "Email")?.Value;

        student.Gender = gender;
        student.IsDorms = isDorms;
        student.StudentId = studentId;

        return student;
    }

    private static string GenerateToken(Student student)
    {
        var claims = new[]
        {
            new Claim("PK_Student", student.PK_Student.ToString()),
            new Claim("FirstName", student.FirstName),
            new Claim("LastName", student.LastName),
            new Claim("Email", student.Email),
            new Claim("StudentId", student.StudentId.ToString()),
            new Claim("Gender", student.Gender.ToString()),
            new Claim("IsDorms", student.IsDorms.ToString()),
            new Claim(ClaimTypes.Role, "Student")
        };

        return GenerateToken(claims);
    }

    private static string GenerateToken(Admin admin)
    {
        var claims = new[]
        {
            new Claim("PK_Admin", admin.PK_Admin.ToString()),
            new Claim("FirstName", admin.FirstName),
            new Claim("LastName", admin.LastName),
            new Claim("Email", admin.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        return GenerateToken(claims);
    }

    private static string GenerateToken(Claim[] claims)
    {
        var jwtKey = Environment.GetEnvironmentVariable("ASPNETCORE_JWTKEY");
        var jwtIssuer = Environment.GetEnvironmentVariable("ASPNETCORE_JWTISSUER");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            jwtIssuer,
            jwtIssuer,
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private Student? AuthenticateStudent(SignInDto signIn)
    {
        var student = DataAccessLayer.GetStudentByEmail(dbContext, signIn.Email);
        if (student != null &&
            BCrypt.Net.BCrypt.Verify(signIn.Password, student.HashedPassword))
            return student;

        return null;
    }

    private Admin? AuthenticateAdmin(SignInDto signIn)
    {
        var admin = DataAccessLayer.GetAdminByEmail(dbContext, signIn.Email);
        if (admin != null && BCrypt.Net.BCrypt.Verify(signIn.Password, admin.HashedPassword))
            return admin;

        return null;
    }
}