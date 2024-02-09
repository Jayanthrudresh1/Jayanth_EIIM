namespace _Net_project.Models;

public class SignupModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
}
