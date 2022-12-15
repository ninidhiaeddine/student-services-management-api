using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Services_Management_App_API.DAL;
using Student_Services_Management_App_API.Dtos;
using Student_Services_Management_App_API.Models;

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

    [HttpPost]
    [Authorize(Roles = "Admin,Student")]
    public async Task<ActionResult> AddReservation([FromBody] ReservationDto dto)
    {
        // check if time slot is fully booked:
        var targetTimeSlot = DataAccessLayer.GetTimeSlotById(dbContext, dto.TimeSlotId);
        if (targetTimeSlot.CurrentCapacity >= targetTimeSlot.MaximumCapacity)
            return Conflict("Failed to book: The requested time slot is fully booked!");

        // add reservation:
        DataAccessLayer.AddReservation(
            dbContext,
            dto.TimeSlotId,
            dto.StudentId);

        // update current capacity in time slot:
        var updated = DataAccessLayer.UpdateTimeSlot(
            dbContext,
            dto.TimeSlotId,
            targetTimeSlot.ServiceType,
            targetTimeSlot.StartTime,
            targetTimeSlot.EndTime,
            targetTimeSlot.MaximumCapacity,
            targetTimeSlot.CurrentCapacity + 1);

        if (!updated)
            return Conflict(
                "Something went wrong with updating the time slot in the database. Cannot increment the current capacity!");

        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "Admins, Students")]
    public async Task<ActionResult> GetReservations(
        [FromQuery] int? studentId,
        [FromQuery] int? timeSlotId)
    {
        if ((studentId.HasValue && timeSlotId.HasValue)
            || (!studentId.HasValue && !timeSlotId.HasValue))
            return BadRequest("You can only specify 'studentId' or 'timeSlotId' at a time.");

        List<Reservation> reservations;

        if (studentId.HasValue)
            reservations = DataAccessLayer.GetReservationsByStudent(dbContext, studentId.Value);
        else if (timeSlotId.HasValue)
            reservations = DataAccessLayer.GetReservationsByTimeSlot(dbContext, timeSlotId.Value);
        else
            return NotFound();

        if (reservations.Capacity > 0)
            return Ok(reservations);

        return NotFound();
    }
}