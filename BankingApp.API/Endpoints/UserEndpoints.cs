using BankingApp.API.Dtos;
using BankingApp.API.Data;
using BankingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.API.Endpoints;

internal static class UserEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => "Welcome!");

        app.MapGet("/users", async (BankingAppContext db) => 
        {
            var users = await db.Users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                Email = u.Email,
                UserType = u.UserType,
                CustomerId = u.CustomerId,
                EmployeeId = u.EmployeeId,
                Status = u.Status,
                LastLoginAt = u.LastLoginAt,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            }).ToListAsync();
            return Results.Ok(users);
        });

        app.MapGet("/users/{id}", async (long id, BankingAppContext db) => 
        {
            var user = await db.Users.FindAsync(id);
            if (user is null) return Results.NotFound();

            var userDto = new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                UserType = user.UserType,
                CustomerId = user.CustomerId,
                EmployeeId = user.EmployeeId,
                Status = user.Status,
                LastLoginAt = user.LastLoginAt,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return Results.Ok(userDto);
        });

        app.MapPost("/users", async (UserDto userDto, BankingAppContext db) =>
        {
            var user = new User
            {
                Username = userDto.Username,
                PasswordHash = userDto.PasswordHash, 
                Email = userDto.Email,
                UserType = userDto.UserType,
                CustomerId = userDto.CustomerId,
                EmployeeId = userDto.EmployeeId,
                Status = userDto.Status ?? "ACTIVE",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            userDto.UserId = user.UserId;
            userDto.CreatedAt = user.CreatedAt;
            userDto.UpdatedAt = user.UpdatedAt;
            userDto.Status = user.Status;

            return Results.Created($"/users/{user.UserId}", userDto);
        });

        app.MapPut("/users/{id}", async (long id, UserDto updatedUser, BankingAppContext db) =>
        {
            var user = await db.Users.FindAsync(id);
            if (user is null)
            {
                return Results.NotFound(new { message = "User not found" });
            }

            user.Username = updatedUser.Username;
            user.PasswordHash = updatedUser.PasswordHash;
            user.Email = updatedUser.Email;
            user.UserType = updatedUser.UserType;
            user.CustomerId = updatedUser.CustomerId;
            user.EmployeeId = updatedUser.EmployeeId;
            user.Status = updatedUser.Status;
            user.LastLoginAt = updatedUser.LastLoginAt;
            user.UpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();

            updatedUser.UserId = user.UserId;
            updatedUser.CreatedAt = user.CreatedAt;
            updatedUser.UpdatedAt = user.UpdatedAt;
            
            return Results.Ok(updatedUser);
        });

        app.MapDelete("/users/{id}", async (long id, BankingAppContext db) =>
        {
            var user = await db.Users.FindAsync(id);
            if (user is null)
            {
                return Results.NotFound(new { message = "User not found" });
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Results.Ok(new { message = "User deleted successfully" });
        });

        app.MapPost("/users/login", async (UserDto loginDto, BankingAppContext db) =>
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.PasswordHash == loginDto.PasswordHash);
            if (user is null)
            {
                return Results.Unauthorized();
            }

            user.LastLoginAt = DateTime.UtcNow;
            await db.SaveChangesAsync();

            var userDto = new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                UserType = user.UserType,
                CustomerId = user.CustomerId,
                EmployeeId = user.EmployeeId,
                Status = user.Status,
                LastLoginAt = user.LastLoginAt,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return Results.Ok(userDto);
        });

        app.MapPost("/users/register", async (UserDto registerDto, BankingAppContext db) =>
        {
            if (await db.Users.AnyAsync(u => u.Username == registerDto.Username))
            {
                return Results.BadRequest(new { message = "Username already exists" });
            }

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = registerDto.PasswordHash, 
                Email = registerDto.Email,
                UserType = registerDto.UserType,
                CustomerId = registerDto.CustomerId,
                EmployeeId = registerDto.EmployeeId,
                Status = registerDto.Status ?? "ACTIVE",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            var userDto = new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                UserType = user.UserType,
                CustomerId = user.CustomerId,
                EmployeeId = user.EmployeeId,
                Status = user.Status,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return Results.Created($"/users/{user.UserId}", userDto);
        });

        return app;
    }
}