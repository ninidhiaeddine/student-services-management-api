using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Services_Management_App_API.DAL;

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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> GetAllAdmins()
    {
        var admins = DataAccessLayer.GetAllAdmins(dbContext);

        if (admins.Capacity > 0)
            return Ok(admins);

        return NotFound();
    }

    [HttpGet("{adminId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> GetAdmin(int adminId)
    {
        var admin = DataAccessLayer.GetAdminById(dbContext, adminId);
        if (admin != null)
            return Ok(admin);

        return NotFound();
    }
}