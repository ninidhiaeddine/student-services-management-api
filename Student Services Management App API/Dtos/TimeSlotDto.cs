namespace Student_Services_Management_App_API.Dtos;

public class TimeSlotDto
{
    public int ServiceType { get; set; }
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public int MaximumCapacity { get; set; }
    public int CurrentCapacity { get; set; } = 0;
}