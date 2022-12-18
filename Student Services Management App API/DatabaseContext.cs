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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // composite key for Reservation:
        modelBuilder.Entity<Reservation>()
            .HasKey(r => new { r.FK_Reservations_Students, r.FK_Reservations_TimeSlots });

        // one-to-many relationship between Student and Reservation:
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Student)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.FK_Reservations_Students);

        // one-to-many relationship between Time Slot and Reservation:
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.TimeSlot)
            .WithMany(ts => ts.Reservations)
            .HasForeignKey(r => r.FK_Reservations_TimeSlots);
    }
}