using System.ComponentModel.DataAnnotations;

namespace Student_Services_Management_App_API.Models;

public class Reservation
{
    [Key] public int PK_Reservation { get; set; }
    public int FK_Reservation_TimeSlot { get; set; }
    public int FK_Reservation_Student { get; set; }
}