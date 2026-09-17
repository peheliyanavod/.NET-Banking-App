using BankingApp.API.Dtos;

namespace BankingApp.API.Endpoints;

internal static class UserEndpoints
{
    private static readonly List<UserDto> users = [
        new UserDto
        {
            UserId = 1,
            Username = "peheliya",
            PasswordHash = "Peheliya123",
            Email = "peheliya@example.com",
            UserType = "CUSTOMER",
            CustomerId = 101,
            Status = "ACTIVE",
            CreatedAt = DateTime.UtcNow
        },
        new UserDto
        {
            UserId = 2,
            Username = "dhanuka_e",
            PasswordHash = "Hashed_Dhanuka123",
            Email = "dhanuka@example.com",
            UserType = "EMPLOYEE",
            EmployeeId = 201,
            Status = "ACTIVE",
            CreatedAt = DateTime.UtcNow
        }
    ];

    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => "Welcome!");

        app.MapGet("/users", () => users);

        app.MapGet("/users/{id}", (long id) => 
        {
            var user = users.Find(user => user.UserId == id);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        });

        app.MapPost("/users", (UserDto user) =>
        {
            var newUser = new UserDto
            {
                UserId = users.Count != 0 ? users.Max(u => u.UserId) + 1 : 1,
                Username = user.Username,
                PasswordHash = user.PasswordHash, 
                Email = user.Email,
                UserType = user.UserType,
                CustomerId = user.CustomerId,
                EmployeeId = user.EmployeeId,
                Status = user.Status ?? "ACTIVE",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            users.Add(newUser);
            return Results.Created($"/users/{newUser.UserId}", newUser);
        });

        app.MapPut("/users/{id}", (long id, UserDto updatedUser) =>
        {
            var index = users.FindIndex(user => user.UserId == id);
            
            if (index == -1)
            {
                return Results.NotFound(new { message = "User not found" });
            }

            users[index] = new UserDto
            {
                UserId = id,
                Username = updatedUser.Username,
                PasswordHash = updatedUser.PasswordHash,
                Email = updatedUser.Email,
                UserType = updatedUser.UserType,
                CustomerId = updatedUser.CustomerId,
                EmployeeId = updatedUser.EmployeeId,
                Status = updatedUser.Status,
                LastLoginAt = updatedUser.LastLoginAt,
                CreatedAt = users[index].CreatedAt, // Preserve original creation date
                UpdatedAt = DateTime.UtcNow
            };
            
            return Results.Ok(users[index]);
        });

        app.MapDelete("/users/{id}", (long id) =>
        {
            var index = users.FindIndex(user => user.UserId == id);
            
            if (index == -1)
            {
                return Results.NotFound(new { message = "User not found" });
            }

            users.RemoveAt(index);
            return Results.Ok(new { message = "User deleted successfully" });
        });

        return app;
    }
    
}