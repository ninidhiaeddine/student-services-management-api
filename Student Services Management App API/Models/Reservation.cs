namespace Student_Services_Management_App_API.Models;

public class Reservation
{
    // composite primary key:
    public int FK_Reservations_TimeSlots { get; set; } // foreign key for Time Slot
    public int FK_Reservations_Students { get; set; } // foreign key for Student

    // navigation properties:
    public TimeSlot TimeSlot { get; set; }
    public Student Student { get; set; }
}