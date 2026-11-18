namespace RigRescue.Models;

public class ServiceRequest
{
    public int Id { get; set; }
    public string ClientId { get; set; }
    public string ClientName { get; set; }
    public int TechnicianProfileId { get; set; }
    public string IssueDescription { get; set; }
    public string Status { get; set; }
    public DateTime RequestDate { get; set; }
}