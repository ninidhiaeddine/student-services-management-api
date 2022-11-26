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

    [HttpGet]
    public async Task<ActionResult> GetReservationById(int reservationId)
    {
        var reservation = DataAccessLayer.GetReservationById(dbContext, reservationId);

        if (reservation != null)
            return Ok(reservation);

        return NotFound();
    }

    [HttpGet]
    public async Task<ActionResult> GetReservations(
        [FromQuery] int studentId = default,
        [FromQuery] int timeSlotId = default)
    {
        List<Reservation> reservations;

        if (studentId != default)
            reservations = DataAccessLayer.GetReservationsByStudent(dbContext, studentId);
        else if (timeSlotId != default)
            reservations = DataAccessLayer.GetTimeSlotReservations(dbContext, timeSlotId);
        else
            return BadRequest("You can only specify 'studentId' or 'timeSlotId' at a time.");

        if (reservations.Capacity > 0)
            return Ok(reservations);

        return NotFound();
    }
}