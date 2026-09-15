using BankingApp.API.Dtos;

namespace BankingApp.API.Endpoints;

internal static class UserEndpoints
{

    private static readonly List<UserDto> users = [
        new UserDto
        {
            Id = 1,
            FirstName = "Peheliya",
            LastName = "Dhanuka",
            Email = "peheliya@example.com",
            Password = "Peheliya123"
        },
        new UserDto
        {
            Id = 2,
            FirstName = "Dhanuka",
            LastName = "Navod",
            Email = "dhanuka@example.com",
            Password = "Dhanuka123"
        }
    ];


    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        
        app.MapGet("/", () => "Welcome!");

        app.MapGet("/users", () => users);

        app.MapGet("/users/{id}", (int id) => users.Find(user => user.Id == id));

        app.MapPost("/users", (UserDto user) =>
        {
            var newUser = new UserDto
            {
                Id = users.Max(u => u.Id) + 1,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password
            };

            users.Add(newUser);
            return newUser;
        });

        app.MapPut("users/{id}", (int id, UserDto updatedUser) =>
        {
            var index = users.FindIndex(user => user.Id == id);

            users[index] = new UserDto
            {
                Id = id,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Email = updatedUser.Email,
                Password = updatedUser.Password
            };
            return users[index];
        });

        app.MapDelete("/users/{id}", (int id) =>
        {
            var index = users.FindIndex(user => user.Id == id);

            users.RemoveAt(index);
            return Results.Ok(new { message = "User deleted successfully" });
        });

        return app;
    }
    
}