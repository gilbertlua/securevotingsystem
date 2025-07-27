namespace SecureVotingSystem.Core.Models;

public class Voter
{
    public int Id { get; set; } // IMPORTANT: Provide a unique Id for each seeded entity
    public string ActivationCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsVoted { get; set; } // This will likely represent "has voted in the current active election" or "has ever voted"
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}