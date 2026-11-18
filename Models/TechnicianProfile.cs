namespace RigRescue.Models;

public class TechnicianProfile
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Specialty { get; set; }
    public string? StudentIdPath { get; set; } 
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; }
}