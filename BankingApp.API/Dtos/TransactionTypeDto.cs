namespace BankingApp.API.Dtos;

public record class TransactionTypeDto
{
    public int TransactionTypeId { get; set; }
    public string? TypeCode { get; set; }
    public string? TypeName { get; set; }
    public string? Description { get; set; }
}