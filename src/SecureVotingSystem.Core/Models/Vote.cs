namespace SecureVotingSystem.Core.Models;

public class Vote
{
    public int Id { get; init; }
    public int VoterId { get; init; } 
    public int CandidateId { get; init; }
    public DateTime VoteTimestamp { get; init; } = DateTime.UtcNow;
    public Voter Voter { get; init; } = null!;
    public Candidate Candidate { get; init; } = null!;
}