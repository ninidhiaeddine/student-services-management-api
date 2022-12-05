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
    public async Task<ActionResult> AddReservation([FromBody] ReservationDto dto)
    {
        DataAccessLayer.AddReservation(
            dbContext,
            dto.TimeSlotId,
            dto.StudentId);

        return Ok();
    }

    [HttpGet("{reservationId:int}")]
    public async Task<ActionResult> GetReservationById(int reservationId)
    {
        var reservation = DataAccessLayer.GetReservationById(dbContext, reservationId);

        if (reservation != null)
            return Ok(reservation);

        return NotFound();
    }

    [HttpGet]
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
        else
            reservations = DataAccessLayer.GetTimeSlotReservations(dbContext, timeSlotId.Value);

        if (reservations.Capacity > 0)
            return Ok(reservations);

        return NotFound();
    }
}