namespace SecureVotingSystem.Application.DTOs;

// Voter DTO
public class VoteDto
{
    public int Id { get; set; }
    public required string ActivationCode { get; set; }
    public required string FullName { get; set; }
}

public class VoterDto
{
    public required string FullName { get; init; }
    public required string PhoneNumber { get; init; }
}