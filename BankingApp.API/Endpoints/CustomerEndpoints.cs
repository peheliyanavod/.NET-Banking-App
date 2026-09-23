using BankingApp.API.Dtos;
using BankingApp.API.Data;
using BankingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.API.Endpoints;

internal static class CustomerEndpoints
{
    public static WebApplication MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGet("/customers", async (BankingAppContext db) =>
        {
            var customers = await db.Customers.Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                NIC = c.NIC,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                BranchID = c.BranchID,
                Status = c.Status,
                CreatedBy = c.CreatedBy,
                CreatedAt = c.CreatedAt
            }).ToListAsync();
            return Results.Ok(customers);
        });
        
        app.MapGet("/customers/{id}", async (int id, BankingAppContext db) =>
        {
            var customer = await db.Customers.FindAsync(id);
            if (customer is null) return Results.NotFound();

            var customerDto = new CustomerDto
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                NIC = customer.NIC,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                BranchID = customer.BranchID,
                Status = customer.Status,
                CreatedBy = customer.CreatedBy,
                CreatedAt = customer.CreatedAt
            };

            return Results.Ok(customerDto);
        });

        app.MapPost("/customers", async (CustomerDto customerDto, BankingAppContext db) =>
        {
            var customer = new Customer
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                NIC = customerDto.NIC,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                Address = customerDto.Address,
                BranchID = customerDto.BranchID,
                Status = customerDto.Status,
                CreatedBy = customerDto.CreatedBy,
                CreatedAt = customerDto.CreatedAt ?? DateTime.UtcNow.ToString("yyyy-MM-dd")
            };

            db.Customers.Add(customer);
            await db.SaveChangesAsync();

            customerDto.CustomerId = customer.CustomerId;
            customerDto.CreatedAt = customer.CreatedAt;

            return Results.Created($"/customers/{customer.CustomerId}", customerDto);
        });

        app.MapPut("/customers/{id}", async (int id, CustomerDto updatedCustomer, BankingAppContext db) =>
        {
            var customer = await db.Customers.FindAsync(id);
            if (customer is null)
            {
                return Results.NotFound(new { message = "Customer not found" });
            }

            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.NIC = updatedCustomer.NIC;
            customer.Email = updatedCustomer.Email;
            customer.Phone = updatedCustomer.Phone;
            customer.Address = updatedCustomer.Address;
            customer.BranchID = updatedCustomer.BranchID;
            customer.Status = updatedCustomer.Status;
            
            await db.SaveChangesAsync();

            updatedCustomer.CustomerId = customer.CustomerId;
            updatedCustomer.CreatedBy = customer.CreatedBy;
            updatedCustomer.CreatedAt = customer.CreatedAt;

            return Results.Ok(updatedCustomer);
        });

        app.MapDelete("customers/{id}", async (int id, BankingAppContext db) =>
        {
            var customer = await db.Customers.FindAsync(id);
            if (customer is null)
            {
                return Results.NotFound(new { message = "Customer not found" });
            }

            db.Customers.Remove(customer);
            await db.SaveChangesAsync();

            return Results.Ok(new {message = "Customer (CustomerID: " + id + ") Deleted successfully!"});
        });
        
        return app;
    }
}