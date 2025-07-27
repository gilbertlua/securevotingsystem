using Microsoft.EntityFrameworkCore;
using SecureVotingSystem.Core.Models; // Ensure this is correct

namespace SecureVotingSystem.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Voter> Voters { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Vote> Votes { get; set; } // Add the DbSet for your new junction table

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Voter Configuration ---
        modelBuilder.Entity<Voter>()
            .HasIndex(v => v.ActivationCode)
            .IsUnique();

        // Seed data for Voter (as you already have)
        modelBuilder.Entity<Voter>().HasData(
            new Voter { Id = 1, ActivationCode = "VOTE001", FullName = "Alice Smith", PhoneNumber = "111-222-3333", IsVoted = false, Created = DateTime.UtcNow, Modified = DateTime.UtcNow },
            new Voter { Id = 2, ActivationCode = "VOTE002", FullName = "Bob Johnson", PhoneNumber = "444-555-6666", IsVoted = false, Created = DateTime.UtcNow, Modified = DateTime.UtcNow },
            new Voter { Id = 3, ActivationCode = "VOTE003", FullName = "Charlie Brown", PhoneNumber = "777-888-9999", IsVoted = true, Created = DateTime.UtcNow, Modified = DateTime.UtcNow }
        );

        // --- Candidate Configuration ---
        modelBuilder.Entity<Candidate>().HasKey(c => c.Id);

        // Seed data for Candidate
        modelBuilder.Entity<Candidate>().HasData(
            new Candidate { Id = 101, Name = "Candidate A", Created = DateTime.UtcNow, Modified = DateTime.UtcNow },
            new Candidate { Id = 102, Name = "Candidate B", Created = DateTime.UtcNow, Modified = DateTime.UtcNow },
            new Candidate { Id = 103, Name = "Candidate C", Created = DateTime.UtcNow, Modified = DateTime.UtcNow }
        );

        // --- Vote Configuration (Junction Table) ---
        modelBuilder.Entity<Vote>()
            .HasKey(v => v.Id); // Define Id as primary key

        // Define the many-to-one relationship from Vote to Voter
        modelBuilder.Entity<Vote>()
            .HasOne(v => v.Voter)
            .WithMany(voter => voter.Votes)
            .HasForeignKey(v => v.VoterId)
            .OnDelete(DeleteBehavior.Restrict); // Or .Cascade, depending on your business rules

        // Define the many-to-one relationship from Vote to Candidate
        modelBuilder.Entity<Vote>()
            .HasOne(v => v.Candidate)
            .WithMany(c => c.Votes)
            .HasForeignKey(v => v.CandidateId)
            .OnDelete(DeleteBehavior.Restrict); // Or .Cascade

        modelBuilder.Entity<Vote>()
            .HasIndex(v => v.VoterId)
            .IsUnique(); 
        modelBuilder.Entity<Vote>().HasData(
            new Vote { Id = 1, VoterId = 1, CandidateId = 101, VoteTimestamp = DateTime.UtcNow.AddMinutes(-10) },
            new Vote { Id = 2, VoterId = 2, CandidateId = 102, VoteTimestamp = DateTime.UtcNow.AddMinutes(-5) }
        );
    }
}