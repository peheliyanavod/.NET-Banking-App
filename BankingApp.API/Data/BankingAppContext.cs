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
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountType> AccountTypes { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
}
