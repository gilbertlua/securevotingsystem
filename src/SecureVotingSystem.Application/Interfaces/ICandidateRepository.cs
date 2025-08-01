using SecureVotingSystem.Core.Models;
using SecureVotingSystem.Application.DTOs;
namespace SecureVotingSystem.Application.Interfaces;

public interface ICandidateRepository
{
    Task<Candidate> Create(CandidateDto candidateDto);
    Task<List<Candidate>> GetAll();
    Task<Candidate> GetById(long id);
}