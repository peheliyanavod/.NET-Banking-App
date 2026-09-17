using BankingApp.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapUserEndpoints();

app.MapCustomerEndpoints();

app.Run();
