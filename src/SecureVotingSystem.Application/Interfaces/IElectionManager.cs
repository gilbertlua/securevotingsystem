﻿using SecureVotingSystem.Core.Models;
using SecureVotingSystem.Application.DTOs;
namespace SecureVotingSystem.Application.Interfaces;

public interface IElectionManager
{
    Task<Vote> ExecutionSelector(string activationCode, long candidateId);
    Task<List<Vote>> GetAll();
    Task<List<Vote>> GetByCandidateId(int candidateId);
}