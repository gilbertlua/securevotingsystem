using Microsoft.EntityFrameworkCore;
using SecureVotingSystem.Application.Interfaces;
using SecureVotingSystem.Core.Models;
using SecureVotingSystem.Infrastructure.Data;

namespace SecureVotingSystem.Infrastructure.Services;

public class ElectionManager(ApplicationDbContext context):IElectionManager
{
    /// <summary>
    /// Get all record
    /// </summary>
    /// <returns>will return list of votes</returns>
    public async Task<List<Vote>> GetAll()
    {
        return await context.Votes.ToListAsync();
    }
    
    /// <summary>
    /// Add new record for execution election
    /// </summary>
    /// <param name="activationCode"> the activation code</param>
    /// <param name="candidateId"> the candidate id</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<Vote> ExecutionSelector(string activationCode, long candidateId)
    {
        ArgumentNullException.ThrowIfNull(activationCode, nameof(activationCode));
        var voter = context.Voters.SingleOrDefault(x => x.ActivationCode == activationCode);
        if (voter == null)
        {
            throw new ArgumentException($"The activation code {activationCode} was not found.");
        }
        var isVoted = voter!.IsVoted;
        if (isVoted)
        {       
            throw new InvalidOperationException($"Voter {voter.PhoneNumber} is already voted");
        }
        ArgumentNullException.ThrowIfNull(candidateId, nameof(candidateId));
        var candidate = context.Candidates.SingleOrDefault(x => x.Id == candidateId);
        var vote = await RecordNewVote(voter.Id, candidate!.Id);
        await context.AddAsync(voter);
        voter.IsVoted = true;
        await context.SaveChangesAsync();
        return vote;
    }

    private async Task<Vote> RecordNewVote(int voterId, int candidateId)
    {
        var recordVotes = new Vote
        {
            VoterId = voterId,
            CandidateId = candidateId,
            VoteTimestamp = DateTime.UtcNow
        };
        await context.Votes.AddAsync(recordVotes);
        await context.SaveChangesAsync();
        return recordVotes;
    }
}