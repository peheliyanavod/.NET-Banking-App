using BankingApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.API.Endpoints;

internal static class AccountEndpoints
{
    public static WebApplication MapAccountEndpoints(this WebApplication app)
    {
        app.MapGet("/accounts", async (BankingAppContext db) =>
        {
            var accounts = await db.Accounts.ToListAsync();
            return Results.Ok(accounts);
        });

        app.MapGet("/accounts/customer/{customerId}", async (long customerId, BankingAppContext db) =>
        {
            var accounts = await db.Accounts
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();
            return Results.Ok(accounts);
        });

        return app;
    }
}
