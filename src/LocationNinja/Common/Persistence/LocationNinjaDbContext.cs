using MongoDB.EntityFrameworkCore.Extensions;

namespace LocationNinja.Common.Persistence;
public class LocationNinjaDbContext : DbContext
{
    public LocationNinjaDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {

    }

    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Location>().ToCollection("Locations");
    }
}
