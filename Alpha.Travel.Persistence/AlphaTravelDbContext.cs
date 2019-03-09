namespace Alpha.Travel.Persistence
{
    using Domain.Entities.Destination;
    using Microsoft.EntityFrameworkCore;

    public class AlphaTravelDbContext : DbContext
    {
        public AlphaTravelDbContext(DbContextOptions<AlphaTravelDbContext> options)
            : base(options)
        {
        }

        public DbSet<Destination> Destinations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyAllConfigurations();
        }
    }
}