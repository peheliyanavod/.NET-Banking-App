using BankingApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.API.Endpoints;

internal static class EmployeeEndpoints
{
    public static WebApplication MapEmployeeEndpoints(this WebApplication app)
    {
        app.MapGet("/employees", async (BankingAppContext db) =>
        {
            var employees = await db.Employees.ToListAsync();
            return Results.Ok(employees);
        });

        return app;
    }
}
