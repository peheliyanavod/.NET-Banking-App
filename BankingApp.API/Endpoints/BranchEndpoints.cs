using BankingApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.API.Endpoints;

internal static class BranchEndpoints
{
    public static WebApplication MapBranchEndpoints(this WebApplication app)
    {
        app.MapGet("/branches", async (BankingAppContext db) =>
        {
            var branches = await db.Branches.ToListAsync();
            return Results.Ok(branches);
        });

        return app;
    }
}
