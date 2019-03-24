namespace Alpha.Travel.WebApi.IntegrationTests
{
    using System;
    using Domain.Entities;
    using Persistence;

    public static class SeedData
    {
        public static void PopulateTestData(AlphaTravelDbContext context)
        {
            var destinations = new[]
            {
                new Destination { Name = "London", Id = 1, Description = "Capital city of UK", CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Destination { Name = "New York", Id = 2, Description = "Fast paced and fun", CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Destination { Name = "Sydney", Id = 3, Description = "Modren and fun loving city", CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" },
                new Destination { Name = "Abu Dhabi", Id = 4, Description = "Modren and fun loving city", CreatedOn = DateTime.UtcNow, CreatedBy = "SeedDataService" }
            };

            context.Destinations.AddRange(destinations);

            context.SaveChanges();
        }
    }
}