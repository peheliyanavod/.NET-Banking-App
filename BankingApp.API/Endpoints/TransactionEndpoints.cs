using BankingApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.API.Endpoints;

internal static class TransactionEndpoints
{
    public static WebApplication MapTransactionEndpoints(this WebApplication app)
    {
        app.MapGet("/transactions", async (BankingAppContext db) =>
        {
            var transactions = await db.Transactions.ToListAsync();
            return Results.Ok(transactions);
        });

        app.MapGet("/transactions/account/{accountId}", async (long accountId, BankingAppContext db) =>
        {
            var transactions = await db.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
            return Results.Ok(transactions);
        });

        app.MapPost("/transactions/transfer", async (BankingApp.API.Dtos.TransferDto transferDto, BankingAppContext db) =>
        {
            if (transferDto.Amount <= 0) return Results.BadRequest("Amount must be greater than zero.");

            using var transaction = await db.Database.BeginTransactionAsync();
            try
            {
                var fromAccount = await db.Accounts.FindAsync(transferDto.FromAccountId);
                if (fromAccount == null) return Results.NotFound("Source account not found.");
                if (fromAccount.Balance < transferDto.Amount) return Results.BadRequest("Insufficient funds.");

                var toAccount = await db.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == transferDto.ToAccountNumber);
                if (toAccount == null) return Results.NotFound("Destination account not found.");

                fromAccount.Balance -= transferDto.Amount;
                toAccount.Balance += transferDto.Amount;

                var debitTransaction = new Models.Transaction
                {
                    AccountId = fromAccount.AccountId,
                    TransactionTypeId = 2, // Assuming 2 is DEBIT
                    Amount = transferDto.Amount,
                    BalanceBefore = fromAccount.Balance + transferDto.Amount,
                    BalanceAfter = fromAccount.Balance,
                    Description = $"Transfer to {toAccount.AccountNumber}: {transferDto.Description}",
                    TransactionDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    Status = "COMPLETED"
                };

                var creditTransaction = new Models.Transaction
                {
                    AccountId = toAccount.AccountId,
                    TransactionTypeId = 1, // Assuming 1 is CREDIT
                    Amount = transferDto.Amount,
                    BalanceBefore = toAccount.Balance - transferDto.Amount,
                    BalanceAfter = toAccount.Balance,
                    Description = $"Transfer from {fromAccount.AccountNumber}: {transferDto.Description}",
                    TransactionDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    Status = "COMPLETED"
                };

                db.Transactions.Add(debitTransaction);
                db.Transactions.Add(creditTransaction);
                await db.SaveChangesAsync();

                await transaction.CommitAsync();

                return Results.Ok(new { message = "Transfer successful." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Results.Problem(ex.Message);
            }
        });

        return app;
    }
}
