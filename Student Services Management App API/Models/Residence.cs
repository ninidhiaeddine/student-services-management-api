using System.ComponentModel.DataAnnotations;

namespace Student_Services_Management_App_API.Models;

public class Residence
{
    [Key] public int PK_Residence { get; set; }
    public string Name { get; set; }
}