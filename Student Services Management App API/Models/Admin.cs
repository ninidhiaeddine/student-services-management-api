using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Student_Services_Management_App_API.Models;

public class Admin
{
    [JsonPropertyName("PK_Admin")] [Key] public int PK_Admin { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? HashedPassword { get; set; }
}