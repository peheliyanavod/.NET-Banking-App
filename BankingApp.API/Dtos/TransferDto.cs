namespace BankingApp.API.Dtos;

public record class TransferDto
{
    public long FromAccountId { get; set; }
    public string? ToAccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}
