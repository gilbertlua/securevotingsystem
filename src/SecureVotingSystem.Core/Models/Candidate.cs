namespace SecureVotingSystem.Core.Models;

public class Candidate
{
    public int Id { get; set; } // Or Guid Id
    public required string  Name { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}