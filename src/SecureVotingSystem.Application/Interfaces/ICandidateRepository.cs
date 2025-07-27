using SecureVotingSystem.Core.Models;
namespace SecureVotingSystem.Application.Interfaces;

public interface ICandidateRepository
{
    Task<Candidate> Create(Candidate candidate);
    Task<List<Candidate>> GetAll();
    Task<Candidate> GetById(long id);
}