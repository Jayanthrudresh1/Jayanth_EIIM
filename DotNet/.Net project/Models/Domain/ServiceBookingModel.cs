namespace _Net_project.Models;

public class ServiceBookingModel
{
    public Guid Id { get; set; }
    public string PatientName { get; set; }
    public int PatientAge { get; set; }
    public string DoctorName { get; set; }
    public string ServiceType { get; set; }
    public DateTime PreferredDate { get; set; }
    public DateTime PreferredTime { get; set; }
}
