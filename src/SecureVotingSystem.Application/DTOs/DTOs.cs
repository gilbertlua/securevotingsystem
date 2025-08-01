namespace SecureVotingSystem.Application.DTOs;

// Voter DTO
public class VoteDto
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    public required string VoterName { get; set; }
}

public class VoterDto
{
    public required string FullName { get; init; }
    public required string PhoneNumber { get; init; }
}

public class CandidateDto
{
    public required string FullName { get; init; }
}
