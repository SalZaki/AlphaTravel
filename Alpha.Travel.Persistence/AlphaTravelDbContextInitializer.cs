namespace Alpha.Travel.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entities;

    public sealed class AlphaTravelDbContextInitializer
    {
        private readonly Dictionary<int, Destination> Destinations = new Dictionary<int, Destination>();

        public static void Initialize(AlphaTravelDbContext context)
        {
            var initializer = new AlphaTravelDbContextInitializer();
            initializer.SeedEverything(context);
        }

        private void SeedEverything(AlphaTravelDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Destinations.Any())
            {
                return;
            }

            SeedDestinations(context);

            SeedCustomers(context);

        }

        private void SeedDestinations(AlphaTravelDbContext context)
        {
            var destinations = new[]
            {
                new Destination { Name = "London", Id = 1, Description = "Capital city of UK", CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Destination { Name = "New York", Id = 2, Description = "Fast paced and fun", CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Destination { Name = "Sydney", Id = 3, Description = "Modren and fun loving city", CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Destination { Name = "Dubai", Id = 4, Description = "Shopping and holidays fun city", CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" }
            };

            context.Destinations.AddRange(destinations);

            context.SaveChanges();
        }

        private void SeedCustomers(AlphaTravelDbContext context)
        {
            var customers = new[]
            {
                new Customer { Firstname = "John",Surname="Richard", Email="test@test.com",Password="password123", Id = 1, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "Recardo",Surname="Smith", Email="test@test.com",Password="password123", Id = 2, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "Mike",Surname="Kurt", Email="test@test.com",Password="password123", Id = 3, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "Sally",Surname="Smith", Email="test@test.com",Password="password123", Id = 4, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "John",Surname="Richard", Email="test@test.com",Password="password123", Id = 5, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" }
            };

            context.Customers.AddRange(customers);

            context.SaveChanges();
        }

    }
}