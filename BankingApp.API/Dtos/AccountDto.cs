namespace BankingApp.API.Dtos;

public record class AccountDto
{
    public long AccountId { get; set; }
    public string? AccountNumber { get; set; }
    public long CustomerId { get; set; }
    public int AccountTypeId { get; set; }
    public long BranchId { get; set; }
    public decimal Balance { get; set; }
    public string? Currency { get; set; }
    public string? Status { get; set; }
    public DateTime? OpenedDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}