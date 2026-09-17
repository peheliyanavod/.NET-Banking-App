namespace BankingApp.API.Dtos;

public record class AccountTypeDto
{
    public int AccountTypeId { get; set; }
    public string? TypeName { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}