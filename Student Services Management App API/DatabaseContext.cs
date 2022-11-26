using Microsoft.EntityFrameworkCore;
using Student_Services_Management_App_API.Models;

namespace Student_Services_Management_App_API;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Residence> Residences { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }
}