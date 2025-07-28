using SecureVotingSystem.Core.Models;
namespace SecureVotingSystem.Application.Interfaces;

public interface IElectionManager
{
    Task<Vote> ExecutionSelector(string activationCode, long candidateId);
    Task<List<Vote>> GetAll();
}