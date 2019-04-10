namespace Alpha.Travel.Persistence
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AlphaTravelDbContext : DbContext
    {
        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public AlphaTravelDbContext(DbContextOptions<AlphaTravelDbContext> options)
            : base(options) { }
    }
}