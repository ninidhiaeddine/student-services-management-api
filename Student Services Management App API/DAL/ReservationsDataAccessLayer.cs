using Microsoft.EntityFrameworkCore;
using Student_Services_Management_App_API.Models;

namespace Student_Services_Management_App_API.DAL;

public partial class DataAccessLayer
{
    public static void AddReservation(
        DatabaseContext db,
        int timeSlotId,
        int studentId)
    {
        var reservation = new Reservation();
        reservation.FK_Reservations_TimeSlots = timeSlotId;
        reservation.FK_Reservations_Students = studentId;

        db.Reservations.Add(reservation);
        db.SaveChanges();
    }

    public static List<Reservation> GetReservationsByStudent(DatabaseContext db, int studentId)
    {
        var reservations = db.Reservations
            .Where(r => r.FK_Reservations_Students == studentId)
            .Include(r => r.TimeSlot)
            .Include(r => r.Student)
            .ToList();

        return reservations;
    }

    public static List<Reservation> GetReservationsByTimeSlot(DatabaseContext db, int timeSlotId)
    {
        var reservations = db.Reservations
            .Where(r => r.FK_Reservations_TimeSlots == timeSlotId)
            .Include(r => r.TimeSlot)
            .Include(r => r.Student)
            .ToList();

        return reservations;
    }
}