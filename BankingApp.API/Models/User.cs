namespace BankingApp.API.Models;

public class User
{
    public long UserId { get; set; }
    public string? Username { get; set; }
    public string? PasswordHash { get; set; } 
    public string? Email { get; set; }
    public string? UserType { get; set; }
    public long? CustomerId { get; set; }
    public long? EmployeeId { get; set; }
    public string? Status { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
