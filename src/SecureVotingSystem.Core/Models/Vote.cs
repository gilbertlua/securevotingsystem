namespace SecureVotingSystem.Core.Models;

public class Vote
{
    public int Id { get; set; }
    public int VoterId { get; set; } 
    public int CandidateId { get; set; }
    public DateTime VoteTimestamp { get; set; } = DateTime.UtcNow;
    public Voter Voter { get; set; } = null!;
    public Candidate Candidate { get; set; } = null!;
}