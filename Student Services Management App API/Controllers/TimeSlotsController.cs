using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TimeSlot>> AddTimeSlots([FromBody] List<TimeSlotDto> dtos)
    {
        var timeSlots = DtosToTimeSlots(dtos);
        DataAccessLayer.AddTimeSlots(dbContext, timeSlots);

        return Ok();
    }

    [HttpGet]
    [Route("{timeSlotId}")]
    [Authorize(Roles = "Admin,Student")]
    public async Task<ActionResult> GetTimeSlotById(int timeSlotId)
    {
        var timeSlot = DataAccessLayer.GetTimeSlotById(dbContext, timeSlotId);
        if (timeSlot == null)
            return NotFound();

        return Ok(timeSlot);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Student")]
    public async Task<ActionResult> GetTimeSlots(
        [FromQuery] int serviceType,
        [FromQuery] DateTime? startDateInclusive,
        [FromQuery] DateTime? endDateExclusive)
    {
        List<TimeSlot> timeSlots;

        if (startDateInclusive.HasValue && endDateExclusive.HasValue)
            timeSlots = DataAccessLayer.GetTimeSlotsWithinDateRange(
                dbContext,
                serviceType,
                startDateInclusive.Value,
                endDateExclusive.Value);
        else
            timeSlots = DataAccessLayer.GetTimeSlotsByServiceType(dbContext, serviceType);

        if (timeSlots.Capacity == 0)
            return NotFound();

        return Ok(timeSlots);
    }

    [HttpPut("{timeSlotId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateTimeSlot([FromBody] TimeSlotDto dto, [FromQuery] int timeSlotId)
    {
        var success = DataAccessLayer.UpdateTimeSlot(
            dbContext,
            timeSlotId,
            dto.ServiceType,
            dto.StartTime,
            dto.EndTime,
            dto.MaximumCapacity,
            dto.CurrentCapacity
        );

        if (success)
            return Ok();

        return NotFound();
    }

    private static List<TimeSlot> DtosToTimeSlots(List<TimeSlotDto> dtos)
    {
        var list = new List<TimeSlot>();
        foreach (var dto in dtos)
        {
            var timeSlot = new TimeSlot();

            timeSlot.ServiceType = dto.ServiceType;
            timeSlot.StartTime = dto.StartTime;
            timeSlot.EndTime = dto.EndTime;
            timeSlot.MaximumCapacity = dto.MaximumCapacity;
            timeSlot.CurrentCapacity = dto.CurrentCapacity;

            list.Add(timeSlot);
        }

        return list;
    }
}