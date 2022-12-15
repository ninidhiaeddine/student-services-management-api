using System.ComponentModel.DataAnnotations;

namespace Student_Services_Management_App_API.Dtos;

public class TimeSlotDto
{
    [Required(ErrorMessage = "Please enter the service type")]
    public int ServiceType { get; set; }

    [Required(ErrorMessage = "Please enter the slot's start time")]
    public string? StartTime { get; set; }

    [Required(ErrorMessage = "Please enter the slot's end time")]
    public string? EndTime { get; set; }

    [Required(ErrorMessage = "Please enter the maximum capacity")]
    public int MaximumCapacity { get; set; }

    [Required(ErrorMessage = "Please enter the current capacity")]
    public int CurrentCapacity { get; set; } = 0;
}