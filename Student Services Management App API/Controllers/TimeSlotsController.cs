using Microsoft.AspNetCore.Mvc;

namespace Student_Services_Management_App_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TimeSlotsController : ControllerBase
{
    private readonly DatabaseContext dbContext;

    public TimeSlotsController(DatabaseContext context)
    {
        dbContext = context;
    }
}