namespace BankingApp.API.Models;

public class Branch
{
    public long BranchId { get; set; }
    public string? BranchCode { get; set; }
    public string? BranchName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
