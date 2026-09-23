namespace BankingApp.API.Models;

public class Transaction
{
    public long TransactionId { get; set; }
    public string? TransactionReference { get; set; }
    public long AccountId { get; set; }
    public int TransactionTypeId { get; set; }
    public decimal Amount { get; set; }
    public decimal BalanceBefore { get; set; }
    public decimal BalanceAfter { get; set; }
    public string? Description { get; set; }
    public DateTime? TransactionDate { get; set; }
    public long PerformedBy { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
}
