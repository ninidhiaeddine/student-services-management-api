using Microsoft.AspNetCore.Mvc;

namespace Student_Services_Management_App_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly DatabaseContext dbContext;

    public ReservationsController(DatabaseContext context)
    {
        dbContext = context;
    }
}