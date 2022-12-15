using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Student_Services_Management_App_API.Models;

public class Admin : IdentityUser
{
    [JsonPropertyName("PK_Admin")] [Key] public int PK_Admin { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
}