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
                new Destination {
                    Id = 1,
                    Name = "London",
                    Description = "Capital city of UK",
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = "SeedDataService",
                    DestinationType = new DestinationType {
                        Id = 1,
                        Name =    "BoatTours",
                        Description = "Capital city of UK",
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = "SeedDataService"
                    }
                },
                new Destination {
                    Id = 2,
                    Name = "New York",
                    Description = "Fast paced and fun",
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = "SeedDataService",
                    DestinationType = new DestinationType {
                        Id = 1,
                        Name =    "Dining",
                        Description = "Capital city of UK",
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = "SeedDataService"
                    }
                },
                new Destination {
                    Id = 3,
                    Name = "Sydney",
                    Description = "Modren and fun loving city",
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = "SeedDataService",
                    DestinationType = new DestinationType {
                        Id = 1,
                        Name =    "NightTime",
                        Description = "Capital city of UK",
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = "SeedDataService"
                    }
                },
                new Destination {
                    Name = "Dubai",
                    Id = 4,
                    Description = "Shopping and holidays fun city",
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = "SeedDataService",
                    DestinationType = new DestinationType {
                        Id = 1,
                        Name =    "SightsAndLandmarks",
                        Description = "Capital city of UK",
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = "SeedDataService"
                    }
                }
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
                new Customer { Firstname = "John",Surname="Richard", Email="test@test.com",Password="password123", Id = 5, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "John",Surname="Richard", Email="test@test.com",Password="password123", Id = 6, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "Recardo",Surname="Smith", Email="test@test.com",Password="password123", Id = 7, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "Mike",Surname="Kurt", Email="test@test.com",Password="password123", Id = 8, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "Sally",Surname="Smith", Email="test@test.com",Password="password123", Id = 9, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "John",Surname="Richard", Email="test@test.com",Password="password123", Id = 10, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "John",Surname="Richard", Email="test@test.com",Password="password123", Id = 11, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "Recardo",Surname="Smith", Email="test@test.com",Password="password123", Id = 12, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "Mike",Surname="Kurt", Email="test@test.com",Password="password123", Id = 13, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "Sally",Surname="Smith", Email="test@test.com",Password="password123", Id = 14, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Customer { Firstname = "John",Surname="Richard", Email="test@test.com",Password="password123", Id = 15, CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}