using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Student_Services_Management_App_API.Models;

public class Residence
{
    [JsonPropertyName("PK_Residence")]
    [Key]
    public int PK_Residence { get; set; }

    public string Name { get; set; }
}