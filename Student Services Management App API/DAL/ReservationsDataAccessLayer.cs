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
        reservation.FK_Reservation_TimeSlot = timeSlotId;
        reservation.FK_Reservation_Student = studentId;

        db.Reservations.Add(reservation);
        db.SaveChanges();
    }

    public static Reservation? GetReservationById(DatabaseContext db, int reservationId)
    {
        var reservation = db.Reservations
            .FirstOrDefault(r => r.PK_Reservation == reservationId);

        return reservation;
    }

    public static List<Reservation> GetReservationsByStudent(DatabaseContext db, int studentId)
    {
        var reservations = db.Reservations
            .Where(r => r.FK_Reservation_Student == studentId)
            .ToList();

        return reservations;
    }

    public static List<Reservation> GetTimeSlotReservations(DatabaseContext db, int timeSlotId)
    {
        var reservations = db.Reservations
            .Where(r => r.FK_Reservation_TimeSlot == timeSlotId)
            .ToList();

        return reservations;
    }
}