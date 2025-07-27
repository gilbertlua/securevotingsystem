using Microsoft.EntityFrameworkCore;
using SecureVotingSystem.Core.Models;
using SecureVotingSystem.Infrastructure.Data;
using SecureVotingSystem.Application.Interfaces;
namespace SecureVotingSystem.Infrastructure.Services;

public class VoterRepository(ApplicationDbContext _context):IVoterRepository
{
    private IOtpGenerator? _otpGenerator;
    /// <summary>
    /// Get all voters
    /// </summary>
    /// <returns> will return all available voters</returns>
    public async Task<List<Voter>> GetAll()
    {
        return await _context.Voters.ToListAsync();
    }
    
    /// <summary>
    /// Get voters by activationCode
    /// </summary>
    /// <param name="activationCode">the activationCode</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<Voter> GetByActivationCodeAsync(string activationCode)
    {
        bool isAvailable = _context.Voters.Any(x => x.ActivationCode == activationCode);
        if (!isAvailable)
        {
            throw new Exception("No activation code found for given activation code");
        }
        Voter? voter = await _context.Voters.SingleOrDefaultAsync(x => x.ActivationCode == activationCode);
        if (voter == null){throw new Exception("No activation code found for given activation code");}
        return voter;
    }

    public async Task<Voter> Create(Voter voter)
    {
        ArgumentNullException.ThrowIfNull(voter, nameof(voter));
        var phoneExist = await _context.Voters.AnyAsync(x =>
            x.PhoneNumber == voter.PhoneNumber
        );
        
        if (phoneExist)
        {
            throw new InvalidOperationException("Phone number already exists");
        }
        
        // Core logic
        _otpGenerator = new OtpGenerator();
        voter.ActivationCode = $"REG - {_otpGenerator.GenerateOtpCode()}";
        voter.IsVoted = false;
        await _context.Voters.AddAsync(voter);
        await _context.SaveChangesAsync();
        return voter;
    }
    
    
}