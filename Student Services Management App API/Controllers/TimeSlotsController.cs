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

    [HttpPost]
    public async Task<ActionResult<TimeSlot>> AddTimeSlots([FromBody] List<TimeSlotDto> dtos)
    {
        var timeSlots = DtosToTimeSlots(dtos);
        DataAccessLayer.AddTimeSlots(dbContext, timeSlots);

        return Ok();
    }

    [HttpGet("{timeSlotId}")]
    public async Task<ActionResult> GetTimeSlotById(int timeSlotId)
    {
        var timeSlot = DataAccessLayer.GetTimeSlotById(dbContext, timeSlotId);
        if (timeSlot == null)
            return NotFound();

        return Ok(timeSlot);
    }

    [HttpGet("{serviceType}")]
    public async Task<ActionResult> GetTimeSlotsByServiceType(int serviceType)
    {
        var timeSlots = DataAccessLayer.GetTimeSlotsByServiceType(dbContext, serviceType);
        if (timeSlots.Capacity == 0)
            return NotFound();

        return Ok(timeSlots);
    }

    [HttpGet("{serviceType} {startDateInclusive} {endDateExclusive}")]
    public async Task<ActionResult> GetTimeSlotsWithinDateRange(
        int serviceType,
        DateTime startDateInclusive,
        DateTime endDateExclusive)
    {
        var timeSlots = DataAccessLayer.GetTimeSlotsWithinDateRange(
            dbContext,
            serviceType,
            startDateInclusive,
            endDateExclusive);

        if (timeSlots.Capacity == 0)
            return NotFound();

        return Ok(timeSlots);
    }

    [HttpPut("timeSlotId")]
    public async Task<ActionResult> UpdateTimeSlot([FromBody] TimeSlotDto dto, int timeSlotId)
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