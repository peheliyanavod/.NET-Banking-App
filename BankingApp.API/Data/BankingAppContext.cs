using BankingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.API.Data;

public class BankingAppContext : DbContext
{
    public BankingAppContext(DbContextOptions<BankingAppContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
}
