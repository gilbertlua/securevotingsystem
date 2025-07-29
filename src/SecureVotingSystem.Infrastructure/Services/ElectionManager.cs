using Microsoft.EntityFrameworkCore;
using SecureVotingSystem.Application.Interfaces;
using SecureVotingSystem.Core.Models;
using SecureVotingSystem.Infrastructure.Data;

namespace SecureVotingSystem.Infrastructure.Services;

public class ElectionManager(ApplicationDbContext context) : IElectionManager
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
    /// <param name="activationCode"> The voter's activation code</param>
    /// <param name="candidateId"> The ID of the candidate</param>
    /// <returns>The newly recorded Vote entity</returns>
    /// <exception cref="ArgumentException">Thrown if activation code or candidate ID is invalid/not found.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the voter has already cast a vote.</exception>
    public async Task<Vote> ExecutionSelector(string activationCode, long candidateId)
    {
        ArgumentNullException.ThrowIfNull(activationCode, nameof(activationCode));
        if (candidateId <= 0) // Basic validation for candidateId
        {
            throw new ArgumentException("Candidate ID must be a positive value.", nameof(candidateId));
        }
        
        var voter = await context.Voters.SingleOrDefaultAsync(x => x.ActivationCode == activationCode);
        if (voter == null)
        {
            throw new ArgumentException($"The activation code '{activationCode}' was not found.");
        }

        bool hasVotedInDb = await context.Votes.AnyAsync(v => v.VoterId == voter.Id);
        if (hasVotedInDb)
        {
            throw new InvalidOperationException(
                $"Voter with activation code '{activationCode}' has already cast a vote.");
        }

        var candidate = await context.Candidates.SingleOrDefaultAsync(x => x.Id == candidateId);
        if (candidate == null)
        {
            throw new ArgumentException($"Candidate with ID {candidateId} not found.");
        }

        var newVote = new Vote
        {
            VoterId = voter.Id,
            CandidateId = candidate.Id,
            VoteTimestamp = DateTime.UtcNow
        };
        await context.Votes.AddAsync(newVote);
        voter.IsVoted = true;
        await context.SaveChangesAsync();

        return newVote;
    }
    
    /// <summary>
    /// Get candidate result
    /// </summary>
    /// <param name="candidateId"></param>
    /// <returns></returns>
    /// <exception cref="InvalidDataException"></exception>
    public async Task<List<Vote>> GetByCandidateId(int candidateId)
    {
        var isCandidateExist = await context.Candidates.SingleOrDefaultAsync(c => c.Id == candidateId) != null;
        if (!isCandidateExist)
        {
            throw new InvalidDataException("The candidate with ID " + candidateId + " was not found.");
        }
        return await context.Votes.Where(v => v.CandidateId == candidateId).ToListAsync();
    }
}