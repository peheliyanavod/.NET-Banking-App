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

        return app;
    }
}
