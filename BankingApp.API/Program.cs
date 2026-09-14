using BankingApp.API.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<UserDto> users = [
    new UserDto
    {
        FirstName = "Peheliya",
        LastName = "Dhanuka",
        Email = "peheliya@example.com",
        Password = "Peheliya123"
    },
    new UserDto
    {
        FirstName = "Dhanuka",
        LastName = "Navod",
        Email = "dhanuka@example.com",
        Password = "Dhanuka123"
    }
];

app.MapGet("/", () => "Hello World!");
app.MapGet("/users", () => users);
// app.MapGet("/test", () => "Testing successful!");

app.Run();
