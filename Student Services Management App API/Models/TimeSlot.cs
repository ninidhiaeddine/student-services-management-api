using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Student_Services_Management_App_API.Models;

public class TimeSlot
{
    [JsonPropertyName("PK_TimeSlot")]
    [Key]
    public int PK_TimeSlot { get; set; }

    public int ServiceType { get; set; } // {0: Laundry, 1: Cleaning, 2: Gym, 3: Pool}
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int MaximumCapacity { get; set; }
    public int CurrentCapacity { get; set; } = 0;
}