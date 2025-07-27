using Microsoft.EntityFrameworkCore;
using SecureVotingSystem.Core.Models;
namespace SecureVotingSystem.Infrastructure.Data;

public class ApplicationDbContext:DbContext
{   
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Voter> Voters { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Voter>()
            .HasIndex(v => v.ActivationCode)
            .IsUnique();
        
        modelBuilder.Entity<Voter>().HasData(
            new Voter
            {
                Id = 1, // IMPORTANT: Provide a unique Id for each seeded entity
                ActivationCode = "VOTE001",
                FullName = "Alice Smith",
                PhoneNumber = "111-222-3333",
                IsVoted = false,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            },
            new Voter
            {
                Id = 2, // Each seeded entity needs a unique Id
                ActivationCode = "VOTE002",
                FullName = "Bob Johnson",
                PhoneNumber = "444-555-6666",
                IsVoted = false,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            },
            new Voter
            {
                Id = 3,
                ActivationCode = "VOTE003",
                FullName = "Charlie Brown",
                PhoneNumber = "777-888-9999",
                IsVoted = true, // Example of a voter who has already voted
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            }
        );
    }
}