using Microsoft.EntityFrameworkCore;
using Student_Services_Management_App_API.Models;

namespace Student_Services_Management_App_API;

public class DatabaseContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Residence> Residences { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(
            "Server=eu-cdbr-west-03.cleardb.net;Database=heroku_c89291c3e0d17eb;Uid=b79a7b90d0e9fd;Pwd=1ca8623f;");
    }
}