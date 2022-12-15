using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Student_Services_Management_App_API.Models;

public class Student : IdentityUser
{
    [JsonPropertyName("PK_Student")] [Key] public int PK_Student { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int StudentId { get; set; }
    public int Gender { get; set; } // 0 = Male, 1 = Female
    public int IsDorms { get; set; } // 0 = False, 1 = True
    public string HashedPassword { get; set; } = string.Empty;
}