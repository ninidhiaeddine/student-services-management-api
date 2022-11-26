using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Services_Management_App_API.DAL;
using Student_Services_Management_App_API.Models;

namespace Student_Services_Management_App_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminsController : ControllerBase
{
    private readonly DatabaseContext dbContext;

    public AdminsController(DatabaseContext context)
    {
        dbContext = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAdmins()
    {
        var admins = DataAccessLayer.GetAllAdmins(dbContext);

        if (admins.Capacity > 0)
            return Ok(admins);

        return NotFound();
    }

    [HttpGet("{adminId}")]
    public async Task<ActionResult> GetAdmin(int adminId)
    {
        var admin = DataAccessLayer.GetAdminById(dbContext, adminId);
        if (admin != null)
            return Ok(admin);

        return NotFound();
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult> GetMe()
    {
        var me = GetCurrentUser();

        if (me != null)
            return Ok(me);

        return NotFound();
    }

    private Admin? GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity == null)
            return null;

        var claims = identity.Claims;

        var admin = new Admin();
        admin.FirstName = claims.FirstOrDefault(c => c.Type == "FirstName")?.Value;
        admin.LastName = claims.FirstOrDefault(c => c.Type == "LastName")?.Value;
        admin.Email = claims.FirstOrDefault(c => c.Type == "Email")?.Value;

        return admin;
    }
}