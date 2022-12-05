namespace Student_Services_Management_App_API.Dtos;

public class TimeSlotDto
{
    public int ServiceType { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int MaximumCapacity { get; set; }
    public int CurrentCapacity { get; set; } = 0;
}