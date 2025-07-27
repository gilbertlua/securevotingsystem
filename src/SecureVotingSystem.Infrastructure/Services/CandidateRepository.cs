using Microsoft.EntityFrameworkCore;
using SecureVotingSystem.Application.Interfaces;
using SecureVotingSystem.Core.Models;
using SecureVotingSystem.Infrastructure.Data;

namespace SecureVotingSystem.Infrastructure.Services;

public class CandidateRepository(ApplicationDbContext _context):ICandidateRepository
{
    
    /// <summary>
    /// Get all candidates
    /// </summary>
    /// <returns></returns>
    public async Task<List<Candidate>> GetAll()
    {
        return await _context.Candidates.ToListAsync();
    }
    /// <summary>
    /// Get By ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<Candidate> GetById(long id)
    {
        var candidate = await _context.Candidates.FindAsync(id);
        if (candidate == null)
        {
            throw new InvalidOperationException($"Candidate with ID {id} not found.");
        }
        return candidate;
    }
    /// <summary>
    /// Create new candidate
    /// </summary>
    /// <param name="candidate"></param>
    /// <returns></returns>
    public async Task<Candidate> Create(Candidate candidate)
    {
        ArgumentNullException.ThrowIfNull(candidate, nameof(candidate));
        await _context.Candidates.AddAsync(candidate);
        return candidate;
    }
}