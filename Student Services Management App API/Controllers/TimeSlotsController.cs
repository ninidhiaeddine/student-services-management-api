using Microsoft.AspNetCore.Mvc;
using Student_Services_Management_App_API.DAL;
using Student_Services_Management_App_API.Dtos;
using Student_Services_Management_App_API.Models;

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

    [HttpPost]
    public async Task<ActionResult<TimeSlot>> AddTimeSlot([FromBody] TimeSlotDto dto)
    {
        DataAccessLayer.AddTimeSlot(
            dbContext,
            dto.ServiceType,
            dto.StartTime,
            dto.EndTime,
            dto.MaximumCapacity,
            dto.CurrentCapacity
        );

        return Ok();
    }
}