using BankingApp.API.Endpoints;
using BankingApp.API.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

builder.Services.AddDbContext<BankingAppContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapUserEndpoints();

app.MapCustomerEndpoints();

app.Run();

