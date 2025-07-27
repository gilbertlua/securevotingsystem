using SecureVotingSystem.Core.Models;

namespace SecureVotingSystem.Application.Interfaces;

public interface IVoterRepository
{
    Task<Voter> GetByActivationCodeAsync(string activationCode);
    Task<Voter> Create(Voter voter);
    Task<List<Voter>> GetAll();
}