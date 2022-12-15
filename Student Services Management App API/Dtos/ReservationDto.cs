using System.ComponentModel.DataAnnotations;

namespace Student_Services_Management_App_API.Dtos;

public class ReservationDto
{
    [Required] public int StudentId { get; set; }

    [Required] public int TimeSlotId { get; set; }
}