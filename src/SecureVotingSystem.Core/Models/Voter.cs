namespace SecureVotingSystem.Core.Models;

public class Voter
{
    public int Id { get; init; }
    public string? ActivationCode { get; set; }
    public required string FullName { get; init; }
    public required string PhoneNumber { get; init; }
    public bool IsVoted { get; set; }
    public DateTime Created { get; init; }
    public DateTime Modified { get; init; }
}