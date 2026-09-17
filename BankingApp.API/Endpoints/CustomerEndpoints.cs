using BankingApp.API.Dtos;

namespace BankingApp.API.Endpoints;

internal static class CustomerEndpoints
{

    private static readonly List<CustomerDto> customers =
    [
        new CustomerDto
        {
            CustomerId = 1,
            FirstName = "Peheliya",
            LastName = "Dhanuka",
            NIC = "200012345678",
            Email = "peheliya@example.com",
            Phone = "0712345678",
            Address = "25 Galle Road, Colombo 03",
            BranchID = "BR001",
            Status = "ACTIVE",
            CreatedBy = "admin",
            CreatedAt = "2026-01-10"
        },
        new CustomerDto
        {
            CustomerId = 2,
            FirstName = "Kasun",
            LastName = "Perera",
            NIC = "199512345678",
            Email = "kasun.perera@example.com",
            Phone = "0773456789",
            Address = "45 Kandy Road, Kadawatha",
            BranchID = "BR002",
            Status = "ACTIVE",
            CreatedBy = "admin",
            CreatedAt = "2026-01-12"
        },
        new CustomerDto
        {
            CustomerId = 3,
            FirstName = "Nimali",
            LastName = "Fernando",
            NIC = "199845612378",
            Email = "nimali.fernando@example.com",
            Phone = "0764567890",
            Address = "18 Temple Road, Kandy",
            BranchID = "BR003",
            Status = "ACTIVE",
            CreatedBy = "employee01",
            CreatedAt = "2026-01-15"
        }
    ];



    public static WebApplication MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGet("/customers", () => customers);
        
        app.MapGet("/customers/{id}", (int id) =>
        {
            var customer = customers.Find(customer => customer.CustomerId == id);

            return customer;
        });

        app.MapPost("/customers", (CustomerDto customer) =>
        {
            CustomerDto newCustomer = new CustomerDto
            {
                CustomerId = customers.Max(u => u.CustomerId) + 1,
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

            customers.Add(newCustomer);

            return newCustomer;
        });

        app.MapPut("/customers/{id}", (int id, CustomerDto updatedCustomer) =>
        {
            var index = customers.FindIndex(customer => customer.CustomerId == id);

            customers[index] = new CustomerDto{
                CustomerId = id,
                FirstName = updatedCustomer.FirstName,
                LastName = updatedCustomer.LastName,
                NIC = updatedCustomer.NIC,
                Email = updatedCustomer.Email,
                Phone = updatedCustomer.Phone,
                Address = updatedCustomer.Address,
                BranchID = updatedCustomer.BranchID,
                Status = updatedCustomer.Status,
                CreatedBy = updatedCustomer.CreatedBy,
                CreatedAt = updatedCustomer.CreatedAt
            };

            return customers[index];
        });

        app.MapDelete("customers/{id}", (int id) =>
        {
            var index = customers.FindIndex(customer => customer.CustomerId == id);

            customers.RemoveAt(index);

            return Results.Ok(new {message = "Customer (CustomerID: " + id + ") Deleted successfully!"});
        });
        

        return app;
    }
    
}