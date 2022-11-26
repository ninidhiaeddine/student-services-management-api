using Microsoft.AspNetCore.Mvc;

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
}