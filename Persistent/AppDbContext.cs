using Microsoft.EntityFrameworkCore;
using SpecificationPatternInEfCore.Entity;

namespace SpecificationPatternInEfCore.Persistent;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<Game> Game { get; set; } = null!;
    public DbSet<Genre> Genre { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>().HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.id);
    }
}


public static class SeedData
{
    public static void SeedRecords(AppDbContext appDbContext)
    {
        if (appDbContext.Genre.Any()) return;
        var genres = new List<Genre>
        {
            new() { Name = "Action" },
            new() { Name = "Action" },
            new() { Name = "Adventre" },
            new() { Name = "RPG" },
            new() { Name = "Simulation" },
            new() { Name = "Strategy" },
            new() { Name = "Sports" },
            new() { Name = "MMO" },
            new() { Name = "Racing" },
            new() { Name = "Fighting" },
            new() { Name = "Shooter" }
        };

        foreach (var genre in genres)
        {
            var game = new Game
            {
                Genre = genre,
                Name = $"{genre.Name} Game",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                    
            };

            appDbContext.Game.Add(game);
            appDbContext.SaveChanges();
        }
    }
}