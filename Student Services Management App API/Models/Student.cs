using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Student_Services_Management_App_API.Models;

public class Student
{
    [JsonPropertyName("PK_Student")] [Key] public int PK_Student { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public int StudentId { get; set; }
    public int Gender { get; set; } // 0 = Male, 1 = Female
    public int IsDorms { get; set; } // 0 = False, 1 = True
    public string? HashedPassword { get; set; }

    public ICollection<Reservation> Reservations { get; set; } // navigation property
}