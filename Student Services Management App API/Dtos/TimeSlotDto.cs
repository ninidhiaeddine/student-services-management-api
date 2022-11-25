namespace Student_Services_Management_App_API.Dtos;

public class TimeSlotDto
{
    public string ServiceType { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}