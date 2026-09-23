namespace BankingApp.API.Models;

public class AccountType
{
    public int AccountTypeId { get; set; }
    public string? TypeName { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
